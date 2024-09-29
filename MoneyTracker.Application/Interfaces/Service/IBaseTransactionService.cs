using MoneyTracker.Application.Interfaces.Repository;
using MoneyTracker.Domain.Models;
using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Interfaces.Service
{
    public interface IBaseTransactionService
    {
        public Task<ResponseModel<List<TransactionListDTO>>> ApplyFilterBaseTransactions(MoneyFilterDTO moneyFilterDTO);
    }
}
