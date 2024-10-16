using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Interfaces.Service
{
    public interface IFilterService
    {
        public  Task<IQueryable<Transaction>> FilterByCategory(IQueryable<Transaction> queryable, Category category);
        public  Task<IQueryable<Transaction>> FilterByUser(IQueryable<Transaction> queryable, int userId);
        public  Task<IQueryable<Transaction>> FilterByDate(IQueryable<Transaction> queryable, DateTime startDate, DateTime endDate);
        public  Task<IQueryable<Transaction>> OrderByDateUp(IQueryable<Transaction> queryable);
        public Task<IQueryable<Transaction>> OrderByDateDown(IQueryable<Transaction> queryable);
        public Task<IQueryable<Transaction>> FilterByAmount(IQueryable<Transaction> queryable, decimal start, decimal end);
        public Task<IQueryable<Transaction>> OrderByAmountUp(IQueryable<Transaction> queryable);
        public Task<IQueryable<Transaction>> OrderByAmountDown(IQueryable<Transaction> queryable);
        public Task<List<Transaction>> EndFilter(IQueryable<Transaction> queryable);
    }
}
