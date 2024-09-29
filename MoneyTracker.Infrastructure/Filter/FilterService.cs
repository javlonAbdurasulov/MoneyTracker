using MoneyTracker.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Infrastructure.Filter
{
    public class FilterService<T> where T : class,IDate,ICategory,IAmount
    {
        public static IQueryable<T> FilterByDateRange(IQueryable<T> queryable, DateTime startDate, DateTime endDate)
        {
            return queryable.Where(t => t.Date >= startDate && t.Date <= endDate);
        }

        public static IQueryable<T> FilterByType(IQueryable<T> queryable, string category)
        {
            return queryable.Where(t => t.Category == category);
        }

        public static IQueryable<T> OrderByDateAsc(IQueryable<T> queryable)
        {
            return queryable.OrderBy(t => t.Date);
        }

        public static IQueryable<T> OrderByDateDesc(IQueryable<T> queryable)
        {
            return queryable.OrderByDescending(t => t.Date);
        }

        public static IQueryable<T> OrderByAmountAsc(IQueryable<T> queryable)
        {
            return queryable.OrderBy(t => t.Amount);
        }

        public static IQueryable<T> OrderByAmountDesc(IQueryable<T> queryable)
        {
            return queryable.OrderByDescending(t => t.Amount);
        }

        public static IQueryable<BaseTransaction> MergeAndOrderByDate(IQueryable<Income> incomes, IQueryable<Expense> expenses)
        {
            var transactions = incomes.Cast<BaseTransaction>()
                                      .Concat(expenses.Cast<BaseTransaction>());
            return transactions;
        }

    }
}
