using MoneyTracker.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Interfaces.Service
{
    public interface IMergeFilters<T>:IFilterService<T> where T : class
    {
        public IQueryable<T> MargeCategory(IQueryable<Income> incomes, IQueryable<Expense> expenses);
    }
}
