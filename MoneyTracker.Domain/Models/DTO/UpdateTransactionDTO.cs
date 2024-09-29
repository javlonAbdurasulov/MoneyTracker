using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Domain.Models.DTO
{
    public class UpdateTransactionDTO
    {
        public int Id { get; set; }
        public string Category{ get; set; }
    }
}
