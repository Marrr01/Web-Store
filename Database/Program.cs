using Entities;
using Entities.Orchestrators;

namespace Database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region test
            using (ApplicationContext db = new ApplicationContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();

                var users = new UserOrch(db);
                var baskets = new BasketOrch(db);
                var products = new ProductOrch(db);
                var basketsProducts = new BasketProductOrch(db);

                //var apple = new Product()
                //{
                //    Id = Guid.NewGuid().ToString(),
                //    Name = "Яблоко",
                //    Price = 20.00
                //};
                //products.Create(apple);
                //var t1 = products.Read((p) => p.Name == "Яблоко");
                //Console.WriteLine(t1.First().Name);
                //products.Delete((p) => p.Name == "Яблоко");
                //var t2 = products.Read((p) => p.Name == "Яблоко");



                //var user = new User()
                //{
                //    Id = "amausah",
                //    Password = "drowssap",
                //    IsAdmin = false
                //};
                //users.Create(user);

                //var apple = new Product()
                //{
                //    Id = Guid.NewGuid().ToString(),
                //    Name = "Яблоко",
                //    Price = 20.00
                //};
                //var peach = new Product()
                //{
                //    Id = Guid.NewGuid().ToString(),
                //    Name = "Персик",
                //    Price = 35.00
                //};
                //products.Create(apple);
                //products.Create(peach);

                //var basket = new Basket()
                //{
                //    Id = Guid.NewGuid().ToString(),
                //    User = user,
                //};
                //baskets.Create(basket);

                //var apples = new BasketProduct()
                //{
                //    Id = Guid.NewGuid().ToString(),
                //    ProductAmount = 5,
                //    Basket = basket,
                //    Product = apple
                //};
                //var peaches = new BasketProduct()
                //{
                //    Id = Guid.NewGuid().ToString(),
                //    ProductAmount = 3,
                //    Basket = basket,
                //    Product = peach
                //};
                //basket.BasketProducts = new List<BasketProduct>()
                //{
                //    apples,
                //    peaches
                //};

                //baskets.Update(basket.Id, basket);

                Console.WriteLine("Пользователи:");
                foreach (var u in users.ReadAll())
                {
                    Console.WriteLine($"{u.Id}\t{u.Password}");
                }
                Console.WriteLine();

                Console.WriteLine("Корзины:");
                foreach (var b in baskets.ReadAll())
                {
                    Console.WriteLine($"{b.Id}\tэто корзина пользователя {b.User.Id}");
                }
                Console.WriteLine();

                Console.WriteLine("Товары:");
                foreach (var p in products.ReadAll())
                {
                    Console.WriteLine($"{p.Id}\t{p.Name}");
                }

                Console.WriteLine();
                foreach (var u in users.ReadAll())
                {
                    Console.WriteLine($"{u.Id}\t{u.Password}");
                    foreach (var b in u.Baskets)
                    {
                        Console.WriteLine($"\t{b.Id}");
                        foreach (var bp in b.BasketProducts)
                        {
                            Console.WriteLine($"\t\t{bp.Product.Name}\t{bp.ProductAmount}");
                        }
                    }
                }
            }
            #endregion
        }
    }
}