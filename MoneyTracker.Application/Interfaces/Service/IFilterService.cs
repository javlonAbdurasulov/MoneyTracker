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
        public  IQueryable<T> FilterByCategory(IQueryable<T> queryable, string category);
        public  IQueryable<T> FilterByUser(IQueryable<T> queryable, int userId);
        public  IQueryable<T> FilterByDate(IQueryable<T> queryable, DateTime startDate, DateTime endDate);
        public  IQueryable<T> OrderByDateUp(IQueryable<T> queryable);
        public  IQueryable<T> OrderByDateDown(IQueryable<T> queryable);
        public IQueryable<T> FilterByAmount(IQueryable<T> queryable, decimal start, decimal end);
        public  IQueryable<T> OrderByAmountUp(IQueryable<T> queryable);
        public  IQueryable<T> OrderByAmountDown(IQueryable<T> queryable);
        public  List<T> EndFilter(IQueryable<T> queryable);
        public IQueryable<TransactionListDTO> MargeCategory();
    }
}
