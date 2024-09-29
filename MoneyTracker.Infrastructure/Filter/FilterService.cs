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
    public class FilterService : IFilterService<TransactionListDTO> 
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;

        public FilterService(IIncomeRepository incomeRepository, IExpenseRepository expenseRepository)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
        }

        public async Task<IQueryable<TransactionListDTO>> FilterByUser(IQueryable<TransactionListDTO> queryable, int userId)
        {
            return queryable.Where(x => x.UserId == userId);
        }
        public async Task<IQueryable<TransactionListDTO>> FilterByDate(IQueryable<TransactionListDTO> queryable, DateTime startDate, DateTime endDate)
        {
            return queryable.Where(t => t.Date.Date >= startDate.Date && t.Date.Date <= endDate.Date);
        }
        public async Task<IQueryable<TransactionListDTO>> FilterByCategory(IQueryable<TransactionListDTO> queryable, string category)
        {
            return queryable.Where(t => t.Category == category);
        }

        public async Task<IQueryable<TransactionListDTO>> OrderByDateUp(IQueryable<TransactionListDTO> queryable)
        {
            return queryable.OrderBy(t => t.Date);
        }

        public async Task<IQueryable<TransactionListDTO>> OrderByDateDown(IQueryable<TransactionListDTO> queryable)
        {
            return queryable.OrderByDescending(t => t.Date);
        }

        public async Task<IQueryable<TransactionListDTO>> FilterByAmount(IQueryable<TransactionListDTO> queryable, decimal start, decimal end)
        {
            return queryable.Where(t => t.Amount >= start && t.Amount<= end);
        }

        public async Task<IQueryable<TransactionListDTO>> OrderByAmountUp(IQueryable<TransactionListDTO> queryable)
        {
            return queryable.OrderBy(t => t.Amount);
        }

        public async Task<IQueryable<TransactionListDTO>> OrderByAmountDown(IQueryable<TransactionListDTO> queryable)
        {
            return queryable.OrderByDescending(t => t.Amount);
        }

        public async Task<IQueryable<TransactionListDTO>> MargeCategory()
        {

            var incomes = _incomeRepository.GetQueryable().Select(i => new TransactionListDTO()
            {
                Amount = i.Amount,
                Category = i.Category,
                Comment = i.Comment,
                Date = i.Date,
                Id = i.Id,
                UserId = i.UserId
            });
            var expenses = _expenseRepository.GetQueryable().Select(i => new TransactionListDTO()
            {
                Amount = i.Amount,
                Category = i.Category,
                Comment = i.Comment,
                Date = i.Date,
                Id = i.Id,
                UserId = i.UserId
            });
            var transactions = incomes.Concat(expenses);
            //var transactions = incomes.Cast<BaseTransaction>()
            //                          .Union(expenses.Cast<BaseTransaction>());
            return transactions;
        }
        public async Task<List<TransactionListDTO>> EndFilter(IQueryable<TransactionListDTO> queryable)
        {
            return await queryable.ToListAsync();
        }

    }
}
