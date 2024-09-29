﻿using MoneyTracker.Application.Interfaces.Service;
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

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
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
                Date = expenseDTO.Date
            };
            var responseExpense = await _expenseRepository.CreateAsync(expense);
            if (responseExpense == null)
            {
                return new("ошибка при создании");
            }
            return new(responseExpense);
        }

        public async Task<bool> Delete(int expenseId)
        {
            var responseDelete = await _expenseRepository.DeleteAsync(expenseId);
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
            expenseById.Result = expense;
            var responseExpense = await _expenseRepository.UpdateAsync(expenseById.Result);
            return new(responseExpense);
        }

    }
}
