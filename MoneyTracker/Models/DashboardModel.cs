using MoneyTracker.Domain.Models.Entity;

namespace MoneyTracker.Models
{
    public class DashboardModel
    {
        public User User{ get; set; }
        public List<Income> Incomes{ get; set; }
        public List<Expense> Expenses{ get; set; }
    }
}
