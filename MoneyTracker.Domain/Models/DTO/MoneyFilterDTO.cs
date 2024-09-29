using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Domain.Models.DTO
{
    public class MoneyFilterDTO
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Category { get; set; }
        public decimal AmountStart { get; set; }
        public decimal AmountEnd { get; set; }
        public int UserId { get; set; }

    }
}
