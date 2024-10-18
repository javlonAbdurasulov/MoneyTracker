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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;

        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Category?> GetById(int id)
        {
            Category? category= await _db.Categories.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);
            return category;
        }
    }
}
