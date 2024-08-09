using HotChocolate.Data.Sorting;
using Microsoft.EntityFrameworkCore;
using Models;

namespace GraphQL_App
{
    public class Query
    {
        [UseProjection]
        public IQueryable<User> GetUsers(ApplicationContext db)
        {
            return db.Users.Include(u => u.Baskets)
                           .ThenInclude(b => b.BasketProducts)
                           .ThenInclude(bp => bp.Product);
        }

        [UseProjection]
        public IQueryable<Basket> GetBaskets(ApplicationContext db)
        {
            return db.Baskets.Include(b => b.BasketProducts)
                             .ThenInclude(bp => bp.Product);
        }

        [UseProjection]
        public IQueryable<BasketProduct> GetBasketsProducts(ApplicationContext db)
        {
            return db.BasketsProducts.Include(bp => bp.Product);
        }

        
        [UsePaging(MaxPageSize = 3, IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Product> GetProducts(ApplicationContext db)
        {
            return db.Products;
        }
    }
}