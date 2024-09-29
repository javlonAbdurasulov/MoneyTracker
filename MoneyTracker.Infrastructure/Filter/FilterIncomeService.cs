using MoneyTracker.Application.Interfaces.Service;
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
        public IQueryable<Income> FilterByUser(IQueryable<Income> queryable, int userId)
        {
            return queryable.Where(x => x.UserId == userId);
        }
        public IQueryable<Income> FilterByDate(IQueryable<Income> queryable, DateTime startDate, DateTime endDate)
        {
            return queryable.Where(t => t.Date.Date >= startDate.Date && t.Date.Date <= endDate.Date);
        }
        public IQueryable<Income> FilterByCategory(IQueryable<Income> queryable, string category)
        {
            return queryable.Where(t => t.Category == category);
        }

        public IQueryable<Income> OrderByDateUp(IQueryable<Income> queryable)
        {
            return queryable.OrderBy(t => t.Date);
        }

        public IQueryable<Income> OrderByDateDown(IQueryable<Income> queryable)
        {
            return queryable.OrderByDescending(t => t.Date);
        }

        public IQueryable<Income> FilterByAmount(IQueryable<Income> queryable, decimal start, decimal end)
        {
            return queryable.Where(t => t.Amount >= start && t.Amount <= end);
        }

        public IQueryable<Income> OrderByAmountUp(IQueryable<Income> queryable)
        {
            return queryable.OrderBy(t => t.Amount);
        }

        public IQueryable<Income> OrderByAmountDown(IQueryable<Income> queryable)
        {
            return queryable.OrderByDescending(t => t.Amount);
        }
        public List<Income> EndFilter(IQueryable<Income> queryable)
        {
            return queryable.ToList();
        }

    }
}
