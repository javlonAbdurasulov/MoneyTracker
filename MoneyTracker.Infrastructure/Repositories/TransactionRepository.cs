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
    public class TransactionRepository:ITransactionRepository
    {
        public readonly AppDbContext _db;

        public TransactionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Transaction> CreateAsync(Transaction obj)
        {
            await _db.Transactions.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Transaction? obj = await GetById(id);

            if (obj == null) return false;

            _db.Transactions.Remove(obj);
            int result = await _db.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            var allTransaction = await _db.Transactions.ToListAsync();
            return allTransaction;
        }

        public async Task<Transaction?> GetById(int id)
        {
            Transaction? transaction = await _db.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return transaction;
        }

        public IQueryable<Transaction> GetQueryable()
        {
            return _db.Transactions.AsQueryable();
        }

        public async Task<Transaction> UpdateAsync(Transaction obj)
        {
            _db.Transactions.Update(obj);
            await _db.SaveChangesAsync();
            return obj;
        }
    }
}
