using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Domain.Models.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public decimal Balance{ get; set; }
        public List<User> Incomes { get; set; }
        public List<User> Expenses { get; set; }

    }
}
