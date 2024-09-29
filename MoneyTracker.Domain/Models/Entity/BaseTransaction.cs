using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Domain.Models.Entity
{
    public abstract class BaseTransaction
    {
        public int Id { get; set; }
        public DateTime Date{ get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }

        public abstract decimal Calculate(decimal curranteAmount);
    }
}
