using Microsoft.EntityFrameworkCore;
using MoneyTracker.Application.Interfaces.Service;
using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Infrastructure.Filter
{
    public class FilterExpenseService:IFilterService<Expense>
    {
        public async Task<IQueryable<Expense>> FilterByUser(IQueryable<Expense> queryable, int userId)
        {
            return queryable.Where(x => x.UserId == userId);
        }
        public async Task<IQueryable<Expense>> FilterByDate(IQueryable<Expense> queryable, DateTime startDate, DateTime endDate)
        {
            return queryable.Where(t => t.Date.Date >= startDate.Date && t.Date.Date <= endDate.Date);
        }
        public async Task<IQueryable<Expense>> FilterByCategory(IQueryable<Expense> queryable, string category)
        {
            return queryable.Where(t => t.Category == category);
        }

        public async Task<IQueryable<Expense>> OrderByDateUp(IQueryable<Expense> queryable)
        {
            return queryable.OrderBy(t => t.Date);
        }

        public async Task<IQueryable<Expense>> OrderByDateDown(IQueryable<Expense> queryable)
        {
            return queryable.OrderByDescending(t => t.Date);
        }

        public async Task<IQueryable<Expense>> FilterByAmount(IQueryable<Expense> queryable, decimal start, decimal end)
        {
            return queryable.Where(t => t.Amount >= start && t.Amount <= end);
        }

        public async Task<IQueryable<Expense>> OrderByAmountUp(IQueryable<Expense> queryable)
        {
            return queryable.OrderBy(t => t.Amount);
        }

        public async Task<IQueryable<Expense>> OrderByAmountDown(IQueryable<Expense> queryable)
        {
            return queryable.OrderByDescending(t => t.Amount);
        }
        public async Task<List<Expense>> EndFilter(IQueryable<Expense> queryable)
        {
            return await queryable.ToListAsync();
        }

        public async Task<IQueryable<TransactionListDTO>> MargeCategory()
        {
            throw new NotImplementedException();
        }
    }
}
