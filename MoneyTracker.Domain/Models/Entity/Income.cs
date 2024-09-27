using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Domain.Models.Entity
{
    public class Income : BaseTransaction
    {
        public override decimal Calculate(decimal curranteAmount)
        {
            return curranteAmount + Amount;
        }
    }
}
