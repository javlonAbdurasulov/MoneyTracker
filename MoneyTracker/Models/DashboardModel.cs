using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Domain.Models.Entity;

namespace MoneyTracker.Models
{
    public class DashboardModel
    {
        public User User{ get; set; }
        public MoneyFilterDTO Filter { get; set; }
        public List<Transaction> Transactions{ get; set; }
        public bool categoryIsVisible{ get; set; }
    }
}
