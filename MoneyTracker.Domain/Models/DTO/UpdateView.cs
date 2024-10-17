using MoneyTracker.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Domain.Models.DTO
{
    public class UpdateView
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Category Category { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
