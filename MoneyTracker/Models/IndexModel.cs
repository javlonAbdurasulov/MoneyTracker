using MoneyTracker.Domain.Models.DTO;

namespace MoneyTracker.Models
{
    public class IndexModel
    {
        public int Id{ get; set; }
        public string UserName { get; set; }
        public bool DefaultFilter{ get; set; }
        public MoneyFilterDTO MoneyFilter { get; set; }
    }
}
