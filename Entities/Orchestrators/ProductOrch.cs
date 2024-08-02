using Entities.Models;

namespace Entities.Orchestrators
{
    public class ProductOrch : BaseOrchestrator<Product>
    {
        public ProductOrch(ApplicationContext db) : base(db) => table = db.Products;
    }
}
