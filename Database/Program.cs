using Entities;
using Entities.Models;
using Entities.Orchestrators;

namespace Database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            #region test
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var users = new UserOrch(db);
                users.DeleteAll();
                users.Create(new User()
                {
                    Id = "amausah",
                    Password = "drowssap",
                    IsAdmin = false
                });

                var products = new ProductOrch(db);
                products.Create(new Product()
                {
                    Id = "Яблоко",
                    Name = "Яблоко",
                    Price = 33.00
                });
                products.Create(new Product()
                {
                    Id = "Груша",
                    Name = "Груша",
                    Price = 25.00
                });

                var baskets = new BasketOrch(db);
                baskets.Create(new Basket()
                {
                    Id = Guid.NewGuid().ToString(),
                    User = users.Read("amausah"),
                    Products = new List<Product>()
                    {
                        products.Read("Яблоко"),
                        products.Read("Груша"),
                        products.Read("Яблоко")
                    }
                });

                foreach (var u in users.ReadAll())
                {
                    Console.WriteLine($"{u.Id}\t{u.Password}");
                    foreach (var b in u.Baskets)
                    {
                        Console.WriteLine($"\t{b.Id}");
                        foreach (var p in b.Products)
                        {
                            Console.WriteLine($"\t\t{p.Id}\t{p.Name}");
                        }
                    }
                }
            }
            #endregion
        }
    }
}