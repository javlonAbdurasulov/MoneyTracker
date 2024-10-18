using Microsoft.EntityFrameworkCore;
using MoneyTracker.Application.Interfaces.Repository;
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
    public class FilterService : IFilterService 
    {
        public async Task<IQueryable<Transaction>> FilterByUser(IQueryable<Transaction> queryable, int userId)
        {
            return queryable.Where(x => x.UserId == userId);
        }
        public async Task<IQueryable<Transaction>> FilterByDate(IQueryable<Transaction> queryable, DateTime startDate, DateTime endDate)
        {
            return queryable.Where(t => t.Date.Date >= startDate.Date && t.Date.Date <= endDate.Date);
        }
        public async Task<IQueryable<Transaction>> FilterByCategory(IQueryable<Transaction> queryable, Category category)
        {
            if(category.Id!=null) queryable = queryable.Where(t => t.Category.Id== category.Id);
            
            return queryable.Where(t => t.Category.IsIncome == category.IsIncome);
        }

        public async Task<IQueryable<Transaction>> OrderByDateUp(IQueryable<Transaction> queryable)
        {
            return queryable.OrderBy(t => t.Date);
        }

        public async Task<IQueryable<Transaction>> OrderByDateDown(IQueryable<Transaction> queryable)
        {
            return queryable.OrderByDescending(t => t.Date);
        }

        public async Task<IQueryable<Transaction>> FilterByAmount(IQueryable<Transaction> queryable, decimal start, decimal end)
        {
            return queryable.Where(t => t.Amount >= start && t.Amount<= end);
        }

        public async Task<IQueryable<Transaction>> OrderByAmountUp(IQueryable<Transaction> queryable)
        {
            return queryable.OrderBy(t => t.Amount);
        }

        public async Task<IQueryable<Transaction>> OrderByAmountDown(IQueryable<Transaction> queryable)
        {
            return queryable.OrderByDescending(t => t.Amount);
        }
        public async Task<List<Transaction>> EndFilter(IQueryable<Transaction> queryable)
        {
            return await queryable.Include(x=>x.Category).ToListAsync();
        }

    }
}
