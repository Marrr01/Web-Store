using Entities.Models;

namespace Entities.Orchestrators
{
    public class BasketOrch : BaseOrchestrator<Basket>
    {
        public BasketOrch(ApplicationContext db) : base(db) => table = db.Baskets;
    }
}
