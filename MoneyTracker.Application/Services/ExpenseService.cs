using MoneyTracker.Application.Interfaces.Service;
using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Domain.Models.Entity;
using MoneyTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyTracker.Application.Interfaces.Repository;

namespace MoneyTracker.Application.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IUserService _userService;

        public ExpenseService(IExpenseRepository expenseRepository, IUserService userService)
        {
            _expenseRepository = expenseRepository;
            _userService = userService;
        }

        public Task<ResponseModel<ResponseModel<List<Expense>>>> ApplyFilter(MoneyFilterDTO expenseFilterDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<Expense>> Create(MoneyDTO expenseDTO)
        {
            Expense expense = new Expense()
            {
                Amount = expenseDTO.Amount,
                Category = expenseDTO.Category,
                Comment = expenseDTO.Comment,
                Date = expenseDTO.Date,
                UserId = expenseDTO.UserId
            };
            var responseExpense = await _expenseRepository.CreateAsync(expense);
            if (responseExpense == null)
            {
                return new("ошибка при создании");
            }
            var updatedBalanceUser = await _userService.UpdateBalanceAsync(expense.UserId, 0, expense.Amount);
            if (updatedBalanceUser == null)
            {
                return new(updatedBalanceUser.Error);
            }
            return new(responseExpense);
        }

        public async Task<bool> Delete(int expenseId)
        {
            Expense? DeletedExpense = await _expenseRepository.GetById(expenseId);
            if (DeletedExpense == null)
            {
                return false;
            }
            decimal deletedAmount = DeletedExpense.Amount;

            var responseDelete = await _expenseRepository.DeleteAsync(expenseId);

            if (responseDelete)
            {
                //
                var updatedBalanceUser = await _userService.UpdateBalanceAsync(DeletedExpense.UserId, 0, deletedAmount);
                if (updatedBalanceUser == null)
                {
                    return false;
                }
            }
            return responseDelete;
        }

        public async Task<ResponseModel<Expense>> GetById(int expenseId)
        {
            var responseExpense = await _expenseRepository.GetById(expenseId);
            if (responseExpense == null)
            {
                return new("Расход с таким Id не существует");
            }
            return new(responseExpense);
        }

        public async Task<ResponseModel<Expense>> Update(Expense expense)
        {
            var expenseById = await GetById(expense.Id);
            if (expenseById == null)
            {
                return new(expenseById.Error);
            }
            
            var responseExpense = await _expenseRepository.UpdateAsync(expense);
            //
            var updatedBalanceUser = await _userService.UpdateBalanceAsync(expense.UserId, expenseById.Result.Amount, expense.Amount);
            if (updatedBalanceUser == null)
            {
                return new(updatedBalanceUser.Error);
            }
            return new(responseExpense);
        }

    }
}
