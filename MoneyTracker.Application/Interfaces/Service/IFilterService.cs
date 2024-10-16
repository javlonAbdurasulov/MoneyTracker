using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Interfaces.Service
{
    public interface IFilterService<T> where T : class
    {
        public  Task<IQueryable<T>> FilterByCategory(IQueryable<T> queryable, Category category);
        public  Task<IQueryable<T>> FilterByUser(IQueryable<T> queryable, int userId);
        public  Task<IQueryable<T>> FilterByDate(IQueryable<T> queryable, DateTime startDate, DateTime endDate);
        public  Task<IQueryable<T>> OrderByDateUp(IQueryable<T> queryable);
        public Task<IQueryable<T>> OrderByDateDown(IQueryable<T> queryable);
        public Task<IQueryable<T>> FilterByAmount(IQueryable<T> queryable, decimal start, decimal end);
        public Task<IQueryable<T>> OrderByAmountUp(IQueryable<T> queryable);
        public Task<IQueryable<T>> OrderByAmountDown(IQueryable<T> queryable);
        public Task<List<T>> EndFilter(IQueryable<T> queryable);
    }
}
