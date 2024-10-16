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
    public interface ITransactionService
    {
        public Task<ResponseModel<Transaction>> Create(MoneyDTO income);
        public Task<ResponseModel<Transaction>> Update(Transaction income);
        public Task<ResponseModel<Transaction>> GetById(int incomeId);
        public Task<ResponseModel<List<Transaction>>> ApplyFilter(MoneyFilterDTO incomeFilterDTO);
        public Task<bool> Delete(int incomeId);
        //public Task<ResponseModel<List<Transaction>>> ApplyFilterBaseTransactions(Transaction moneyFilterDTO);
    }
}
