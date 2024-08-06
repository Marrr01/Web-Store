using Models;

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

                //var user = new User()
                //{
                //    Login = "usah",
                //    Password = "password",
                //    IsAdmin = true,
                //    Baskets = new List<Basket>()
                //    {
                //        new Basket()
                //        {
                //            BasketProducts = new List<BasketProduct>()
                //        }
                //    }
                //};
                //db.Users.Add(user);

                //var apple = new Product()
                //{
                //    Name = "Apple",
                //    Price = 20
                //};
                //var peach = new Product()
                //{
                //    Name = "Peach",
                //    Price = 30
                //};
                //db.Products.Add(apple);
                //db.Products.Add(peach);

                //var applesInBasket = new BasketProduct()
                //{
                //    Basket = user.Baskets.First(),
                //    Product = apple,
                //    ProductAmount = 3
                //};
                //var peachesInBasket = new BasketProduct()
                //{
                //    Basket = user.Baskets.First(),
                //    Product = peach,
                //    ProductAmount = 5
                //};
                //user.Baskets.First().BasketProducts.Add(applesInBasket);
                //user.Baskets.First().BasketProducts.Add(peachesInBasket);
                //db.SaveChanges();

                Console.WriteLine("USERS:");
                foreach (var u in db.Users)
                {
                    Console.WriteLine($"{u.Login} {u.Password}");
                }
                Console.WriteLine();

                Console.WriteLine("BASKETS:");
                foreach (var b in db.Baskets)
                {
                    Console.WriteLine($"{b.Id} {b.User.Login}");
                }
                Console.WriteLine();

                Console.WriteLine("BASKETS PRODUCTS:");
                foreach (var bp in db.BasketsProducts)
                {
                    Console.WriteLine($"{bp.Basket.User.Login} {bp.Product.Name} {bp.ProductAmount} шт.");
                }
                Console.WriteLine();

                Console.WriteLine("PRODUCTS:");
                foreach (var p in db.Products)
                {
                    Console.WriteLine($"{p.Id} {p.Name}");
                }
                Console.WriteLine();

                foreach (var u in db.Users)
                {
                    Console.WriteLine($"{u.Login} {u.Password}");
                    foreach (var b in u.Baskets)
                    {
                        Console.WriteLine($"\t{b.Id} {b.User.Login}");
                        foreach (var bp in b.BasketProducts)
                        {
                            Console.WriteLine($"\t\t{bp.Basket.User.Login} {bp.Product.Name} {bp.ProductAmount} шт.");
                            Console.WriteLine($"\t\t\t{bp.Product.Id} {bp.Product.Name}");
                        }
                    }
                }
            }
            #endregion
        }
    }
}