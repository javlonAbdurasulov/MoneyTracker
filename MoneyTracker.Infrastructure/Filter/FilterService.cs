using MoneyTracker.Application.Interfaces.Service;
using MoneyTracker.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Infrastructure.Filter
{
    public class FilterService : IMergeFilters<BaseTransaction> 
    {

        public IQueryable<BaseTransaction> FilterByUser(IQueryable<BaseTransaction> queryable, int userId)
        {
            return queryable.Where(x => x.UserId == userId);
        }
        public IQueryable<BaseTransaction> FilterByDate(IQueryable<BaseTransaction> queryable, DateTime startDate, DateTime endDate)
        {
            return queryable.Where(t => t.Date.Date >= startDate.Date && t.Date.Date <= endDate.Date);
        }
        public IQueryable<BaseTransaction> FilterByCategory(IQueryable<BaseTransaction> queryable, string category)
        {
            return queryable.Where(t => t.Category == category);
        }

        public IQueryable<BaseTransaction> OrderByDateUp(IQueryable<BaseTransaction> queryable)
        {
            return queryable.OrderBy(t => t.Date);
        }

        public IQueryable<BaseTransaction> OrderByDateDown(IQueryable<BaseTransaction> queryable)
        {
            return queryable.OrderByDescending(t => t.Date);
        }

        public IQueryable<BaseTransaction> FilterByAmount(IQueryable<BaseTransaction> queryable, decimal start, decimal end)
        {
            return queryable.Where(t => t.Amount >= start && t.Amount<= end);
        }

        public IQueryable<BaseTransaction> OrderByAmountUp(IQueryable<BaseTransaction> queryable)
        {
            return queryable.OrderBy(t => t.Amount);
        }

        public IQueryable<BaseTransaction> OrderByAmountDown(IQueryable<BaseTransaction> queryable)
        {
            return queryable.OrderByDescending(t => t.Amount);
        }

        public IQueryable<BaseTransaction> MargeCategory(IQueryable<Income> incomes, IQueryable<Expense> expenses)
        {
            var transactions = incomes.Cast<BaseTransaction>()
                                      .Concat(expenses.Cast<BaseTransaction>());
            return transactions;
        }
        public List<BaseTransaction> EndFilter(IQueryable<BaseTransaction> queryable)
        {
            return queryable.ToList();
        }

    }
}
