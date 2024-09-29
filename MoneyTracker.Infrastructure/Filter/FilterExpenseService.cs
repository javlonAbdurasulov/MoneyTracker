using MoneyTracker.Application.Interfaces.Service;
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
        public IQueryable<Expense> FilterByUser(IQueryable<Expense> queryable, int userId)
        {
            return queryable.Where(x => x.UserId == userId);
        }
        public IQueryable<Expense> FilterByDate(IQueryable<Expense> queryable, DateTime startDate, DateTime endDate)
        {
            return queryable.Where(t => t.Date.Date >= startDate.Date && t.Date.Date <= endDate.Date);
        }
        public IQueryable<Expense> FilterByCategory(IQueryable<Expense> queryable, string category)
        {
            return queryable.Where(t => t.Category == category);
        }

        public IQueryable<Expense> OrderByDateUp(IQueryable<Expense> queryable)
        {
            return queryable.OrderBy(t => t.Date);
        }

        public IQueryable<Expense> OrderByDateDown(IQueryable<Expense> queryable)
        {
            return queryable.OrderByDescending(t => t.Date);
        }

        public IQueryable<Expense> FilterByAmount(IQueryable<Expense> queryable, decimal start, decimal end)
        {
            return queryable.Where(t => t.Amount >= start && t.Amount <= end);
        }

        public IQueryable<Expense> OrderByAmountUp(IQueryable<Expense> queryable)
        {
            return queryable.OrderBy(t => t.Amount);
        }

        public IQueryable<Expense> OrderByAmountDown(IQueryable<Expense> queryable)
        {
            return queryable.OrderByDescending(t => t.Amount);
        }
        public List<Expense> EndFilter(IQueryable<Expense> queryable)
        {
            return queryable.ToList();
        }
    }
}
