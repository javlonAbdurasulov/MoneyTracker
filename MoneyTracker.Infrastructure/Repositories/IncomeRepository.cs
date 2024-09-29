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
    public class IncomeRepository : IIncomeRepository
    {
        public readonly AppDbContext _db;

        public IncomeRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Income> CreateAsync(Income obj)
        {
            await _db.Incomes.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Income? obj = await GetById(id);

            if (obj == null) return false;

            _db.Incomes.Remove(obj);
            int result = await _db.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Income>> GetAllAsync()
        {
            var allIncomes = await _db.Incomes.ToListAsync();
            return allIncomes;
        }

        public async Task<Income?> GetById(int id)
        {
            Income? income = await _db.Incomes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return income;
        }

        public IQueryable<Income> GetQueryable()
        {
            return _db.Incomes.AsQueryable();
        }

        public async Task<Income> UpdateAsync(Income obj)
        {
            _db.Incomes.Update(obj);
            await _db.SaveChangesAsync();
            return obj;
        }
    }
}
