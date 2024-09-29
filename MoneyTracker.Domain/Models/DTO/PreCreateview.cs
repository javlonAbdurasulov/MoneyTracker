using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Domain.Models.DTO
{
    public class PreCreateview
    {
        public int UserId { get; set; }
        public string UserName{ get; set; }
        public bool DefaultFilter{ get; set; }
    }
}
