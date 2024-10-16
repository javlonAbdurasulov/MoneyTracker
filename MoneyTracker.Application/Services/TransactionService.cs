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
        private readonly IFilterService<Income> _filterIncomeService;

        public TransactionService(ITransactionRepository transactionRepository, IUserService userService, IFilterService<Income> filterIncomeService)
        {
            _transactionRepository = transactionRepository;
            _userService = userService;
            _filterIncomeService = filterIncomeService;
        }

        public async Task<ResponseModel<List<Transaction>>> ApplyFilter(MoneyFilterDTO transactionFilterDTO)
        {
            var query = _transactionRepository.GetQueryable();
            var filterList = await _filterIncomeService.FilterByUser(query, transactionFilterDTO.UserId);
            filterList = await _filterIncomeService.FilterByCategory(filterList, transactionFilterDTO.Category);
            filterList = await _filterIncomeService.FilterByDate(filterList, transactionFilterDTO.DateStart, transactionFilterDTO.DateEnd);
            filterList = await _filterIncomeService.FilterByAmount(filterList, transactionFilterDTO.AmountStart, transactionFilterDTO.AmountEnd);
            if (transactionFilterDTO.OrderBy == 1)
            {
                filterList = await _filterIncomeService.OrderByDateUp(filterList);
            }
            else if (transactionFilterDTO.OrderBy == 2)
            {
                filterList = await _filterIncomeService.OrderByDateDown(filterList);
            }
            else if (transactionFilterDTO.OrderBy == 3)
            {
                filterList = await _filterIncomeService.OrderByAmountUp(filterList);
            }
            else
            {
                filterList = await _filterIncomeService.OrderByAmountDown(filterList);
            }

            var res = await _filterIncomeService.EndFilter(filterList);
            return new(res);
        }

        public async Task<ResponseModel<Transaction>> Create(MoneyDTO transactionDTO)
        {
            Transaction transaction  = new Transaction()
            {
                Amount = transactionDTO.Amount,
                Category = transactionDTO.Category,
                Comment = transactionDTO.Comment,
                Date = transactionDTO.Date.ToUniversalTime().AddDays(1),
                UserId = transactionDTO.UserId
            };
            var responseTransaction = await _transactionRepository.CreateAsync(transaction);
            if (responseTransaction == null)
            {
                return new("ошибка при создании");
            }
            //Income
            var updatedBalanceUser = await _userService.UpdateBalanceAsync(transaction.UserId, 0, transaction.Amount);
            if (updatedBalanceUser == null)
            {
                return new(updatedBalanceUser.Error);
            }
            return new(responseTransaction);
        }

        public async Task<bool> Delete(int incomeId)
        {
            Transaction? DeletedIncome = await _transactionRepository.GetById(incomeId);
            if (DeletedIncome == null)
            {
                return false;
            }
            decimal deletedAmount = DeletedIncome.Amount;

            var responseDelete = await _transactionRepository.DeleteAsync(incomeId);
            if (responseDelete)
            {
                //
                var updatedBalanceUser = await _userService.UpdateBalanceAsync(DeletedIncome.UserId, deletedAmount, 0);
                if (updatedBalanceUser == null)
                {
                    return false;
                }
            }
            return responseDelete;
        }

        public async Task<ResponseModel<Transaction>> GetById(int incomeId)
        {
            var responseIncome = await _transactionRepository.GetById(incomeId);
            if (responseIncome == null)
            {
                return new("Приход с таким Id не существует");
            }
            return new(responseIncome);
        }

        public async Task<ResponseModel<Transaction>> Update(Transaction income)
        {
            var incomeById = await GetById(income.Id);
            if (incomeById == null)
            {
                return new(incomeById.Error);
            }

            var responseIncome = await _transactionRepository.UpdateAsync(income);
            //
            var updatedBalanceUser = await _userService.UpdateBalanceAsync(income.UserId, incomeById.Result.Amount, income.Amount);
            if (updatedBalanceUser == null)
            {
                return new(updatedBalanceUser.Error);
            }
            return new(responseIncome);
        }
    }
}
