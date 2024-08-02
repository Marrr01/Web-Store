using Entities.Models;

namespace Entities.Orchestrators
{
    public class BasketProductOrch : BaseOrchestrator<BasketProduct>
    {
        public BasketProductOrch(ApplicationContext db) : base(db) => table = db.BasketsProducts;
    }
}
