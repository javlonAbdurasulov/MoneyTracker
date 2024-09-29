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
        public static IQueryable<T> FilterByDate(IQueryable<T> queryable, DateTime startDate, DateTime endDate)
        {
            return queryable.Where(t => t.Date >= startDate && t.Date <= endDate);
        }

        public static IQueryable<T> FilterByCategory(IQueryable<T> queryable, string category)
        {
            return queryable.Where(t => t.Category == category);
        }

        public static IQueryable<T> OrderByDateUp(IQueryable<T> queryable)
        {
            return queryable.OrderBy(t => t.Date);
        }

        public static IQueryable<T> OrderByDateDown(IQueryable<T> queryable)
        {
            return queryable.OrderByDescending(t => t.Date);
        }

        public static IQueryable<T> OrderByAmountUp(IQueryable<T> queryable)
        {
            return queryable.OrderBy(t => t.Amount);
        }

        public static IQueryable<T> OrderByAmountDown(IQueryable<T> queryable)
        {
            return queryable.OrderByDescending(t => t.Amount);
        }

        public static IQueryable<BaseTransaction> MargeCategory(IQueryable<Income> incomes, IQueryable<Expense> expenses)
        {
            var transactions = incomes.Cast<BaseTransaction>()
                                      .Concat(expenses.Cast<BaseTransaction>());
            return transactions;
        }

    }
}
