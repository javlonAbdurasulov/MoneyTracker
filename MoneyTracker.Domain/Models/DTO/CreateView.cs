using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Domain.Models.DTO
{
    public class CreateView
    {
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool DefaultFilter{ get; set; }

    }
}
