using Microsoft.EntityFrameworkCore;
using MoneyTracker.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions):base(dbContextOptions)
        {

        }
        
        public DbSet<User> Users {  get; set; }
        public DbSet<Transaction> Transactions{  get; set; }
        public DbSet<Expense> Expense {  get; set; }

        
    }
}
