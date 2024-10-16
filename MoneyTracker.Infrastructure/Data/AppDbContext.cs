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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category) 
                .WithMany() 
                .HasForeignKey(t => t.CategoryId) 
                .OnDelete(DeleteBehavior.Restrict); 
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users {  get; set; }
        public DbSet<Transaction> Transactions{  get; set; }
        public DbSet<Category> Categories{  get; set; }

        
    }
}
