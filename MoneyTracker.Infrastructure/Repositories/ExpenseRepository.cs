using Microsoft.EntityFrameworkCore;
using MoneyTracker.Application.Interfaces.Repository;
using MoneyTracker.Domain.Models.Entity;
using MoneyTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Infrastructure.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        public readonly AppDbContext _db;

        public ExpenseRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Expense> CreateAsync(Expense obj)
        {
            await _db.Expense.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Expense? obj = await GetById(id);

            if (obj == null) return false;

            _db.Expense.Remove(obj);
            int result = await _db.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Expense>> GetAllAsync()
        {
            DbSet<Expense> res = _db.Expense;
            var allExpenses = await _db.Expense.ToListAsync();
            return allExpenses;
        }

        public async Task<Expense?> GetById(int id)
        {
            Expense? expense = await _db.Expense.FirstOrDefaultAsync(x => x.Id == id);
            return expense;
        }

        public async Task<Expense> UpdateAsync(Expense obj)
        {
            _db.Expense.Update(obj);
            await _db.SaveChangesAsync();
            return obj;
        }
    }
}
