using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Domain.Models.Entity
{
    public class Category
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public bool IsIncome{ get; set; }
        public List<Transaction> Transactions{ get; set; }
    }
}
