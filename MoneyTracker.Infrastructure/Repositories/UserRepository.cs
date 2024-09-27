using Microsoft.EntityFrameworkCore;
using MoneyTracker.Application.Interfaces.Base;
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
    public class UserRepository : IUserRepository
    {
        public readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User> CreateAsync(User obj)
        {
            await _db.Users.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            User? obj = await GetById(id);

            if (obj == null) return false;

            _db.Users.Remove(obj);
            int result = await _db.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var allUsers = await _db.Users.ToListAsync();
            return allUsers;
        }

        public async Task<User?> GetById(int id)
        {
            User? user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<User> UpdateAsync(User obj)
        {
            _db.Users.Update(obj);
            await _db.SaveChangesAsync();
            return obj;
        }
    }
}
