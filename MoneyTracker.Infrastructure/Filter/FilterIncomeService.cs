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
    public class FilterIncomeService:IFilterService<Income>
    {
        public async Task<IQueryable<Income>> FilterByUser(IQueryable<Income> queryable, int userId)
        {
            return queryable.Where(x => x.UserId == userId);
        }
        public async Task<IQueryable<Income>> FilterByDate(IQueryable<Income> queryable, DateTime startDate, DateTime endDate)
        {
            return queryable.Where(t => t.Date.Date >= startDate.Date && t.Date.Date <= endDate.Date);
        }
        public async Task<IQueryable<Income>> FilterByCategory(IQueryable<Income> queryable, string category)
        {
            return queryable.Where(t => t.Category == category);
        }

        public async Task<IQueryable<Income>> OrderByDateUp(IQueryable<Income> queryable)
        {
            return queryable.OrderBy(t => t.Date);
        }

        public async Task<IQueryable<Income>> OrderByDateDown(IQueryable<Income> queryable)
        {
            return queryable.OrderByDescending(t => t.Date);
        }

        public async Task<IQueryable<Income>> FilterByAmount(IQueryable<Income> queryable, decimal start, decimal end)
        {
            return queryable.Where(t => t.Amount >= start && t.Amount <= end);
        }

        public async Task<IQueryable<Income>> OrderByAmountUp(IQueryable<Income> queryable)
        {
            return queryable.OrderBy(t => t.Amount);
        }

        public async Task<IQueryable<Income>> OrderByAmountDown(IQueryable<Income> queryable)
        {
            return queryable.OrderByDescending(t => t.Amount);
        }
        public async Task<List<Income>> EndFilter(IQueryable<Income> queryable)
        {
            return await queryable.ToListAsync();
        }

        public async Task<IQueryable<TransactionListDTO>> MargeCategory()
        {
            throw new NotImplementedException();
        }
    }
}
