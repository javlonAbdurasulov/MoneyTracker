using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Domain.Models.Entity;
using MoneyTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Interfaces.Service
{
    public interface IExpenseService
    {
        public Task<ResponseModel<Expense>> Create(MoneyDTO expense);
        public Task<ResponseModel<Expense>> Update(Expense expense);
        public Task<ResponseModel<Expense>> GetById(int expenseId);
        public Task<ResponseModel<ResponseModel<List<Expense>>>> ApplyFilter(MoneyFilterDTO expenseFilterDTO);
        public Task<bool> Delete(int expenseId);

    }
}
