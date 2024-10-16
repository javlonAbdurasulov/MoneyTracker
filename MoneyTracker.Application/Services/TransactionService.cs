using MoneyTracker.Application.Interfaces.Repository;
using MoneyTracker.Application.Interfaces.Service;
using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Domain.Models.Entity;
using MoneyTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Services
{
    public class TransactionService:ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserService _userService;
        private readonly IFilterService _filterService;

        public TransactionService(ITransactionRepository transactionRepository, IUserService userService, IFilterService filterIncomeService)
        {
            _transactionRepository = transactionRepository;
            _userService = userService;
            _filterService = filterIncomeService;
        }

        public async Task<ResponseModel<List<Transaction>>> ApplyFilter(MoneyFilterDTO transactionFilterDTO)
        {
            var query = _transactionRepository.GetQueryable();
            var filterList = await _filterService.FilterByUser(query, transactionFilterDTO.UserId);
            filterList = await _filterService.FilterByCategory(filterList, transactionFilterDTO.Category);
            filterList = await _filterService.FilterByDate(filterList, transactionFilterDTO.DateStart, transactionFilterDTO.DateEnd);
            filterList = await _filterService.FilterByAmount(filterList, transactionFilterDTO.AmountStart, transactionFilterDTO.AmountEnd);
            if (transactionFilterDTO.OrderBy == 1)
            {
                filterList = await _filterService.OrderByDateUp(filterList);
            }
            else if (transactionFilterDTO.OrderBy == 2)
            {
                filterList = await _filterService.OrderByDateDown(filterList);
            }
            else if (transactionFilterDTO.OrderBy == 3)
            {
                filterList = await _filterService.OrderByAmountUp(filterList);
            }
            else
            {
                filterList = await _filterService.OrderByAmountDown(filterList);
            }

            var res = await _filterService.EndFilter(filterList);
            return new(res);
        }

        public async Task<ResponseModel<Transaction>> Create(MoneyDTO transactionDTO)
        {
            Transaction transaction  = new Transaction()
            {
                Amount = transactionDTO.Amount,
                CategoryId = transactionDTO.Category.Id,
                Comment = transactionDTO.Comment,
                Date = transactionDTO.Date.ToUniversalTime().AddDays(1),
                UserId = transactionDTO.UserId
            };
            var responseTransaction = await _transactionRepository.CreateAsync(transaction);
            if (responseTransaction == null)
            {
                return new("ошибка при создании");
            }
            //--------------------------------------------
            decimal amountMinus = transactionDTO.Category.Name == "Income" ? 0 : transaction.Amount; 
            decimal amountPlus = transactionDTO.Category.Name == "Income" ? transaction.Amount : 0 ; 

            var updatedBalanceUser = await _userService.UpdateBalanceAsync(transaction.UserId, amountMinus, amountPlus);

            if (updatedBalanceUser == null)
            {
                return new(updatedBalanceUser.Error);
            }
            return new(responseTransaction);
        }

        public async Task<bool> Delete(int transactionId)
        {
            Transaction? DeletedTransaction = await _transactionRepository.GetById(transactionId);
            if (DeletedTransaction == null)
            {
                return false;
            }
            decimal deletedAmount = DeletedTransaction.Amount;

            var responseDelete = await _transactionRepository.DeleteAsync(transactionId);
            if (responseDelete)
            {
                //--------------------------------

                decimal amountMinus = DeletedTransaction.Category.Name == "Income" ? deletedAmount : 0;
                decimal amountPlus = DeletedTransaction.Category.Name == "Income" ? 0 : deletedAmount;

                var updatedBalanceUser = await _userService.UpdateBalanceAsync(DeletedTransaction.UserId, amountMinus, amountPlus);
                if (updatedBalanceUser == null)
                {
                    return false;
                }
            }
            return responseDelete;
        }

        public async Task<ResponseModel<Transaction>> GetById(int transactionId)
        {
            var responseTransaction= await _transactionRepository.GetById(transactionId);
            if (responseTransaction== null)
            {
                return new("транзакция с таким Id не существует");
            }
            return new(responseTransaction);
        }

        public async Task<ResponseModel<Transaction>> Update(Transaction transaction)
        {
            var transactionById = await GetById(transaction.Id);
            if (transactionById == null)
            {
                return new(transactionById.Error);
            }

            var responseTransaction = await _transactionRepository.UpdateAsync(transaction);
            //
            //var updatedBalanceUser = await _userService.UpdateBalanceAsync(expense.UserId, expense.Amount, expenseById.Result.Amount);
            var updatedBalanceUser = await _userService.UpdateBalanceAsync(transaction.UserId, transactionById.Result.Amount, transaction.Amount);
            if (updatedBalanceUser == null)
            {
                return new(updatedBalanceUser.Error);
            }
            return new(responseTransaction);
        }
    }
}
