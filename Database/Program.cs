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

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureCreated();

                var orch = new UserOrch(db);
                orch.DeleteAll();
                orch.Create(new User() { Id = "123", Name = "FromConsole", Age = 30 });
                var users = orch.ReadAll();
                Console.WriteLine("Users list:");
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}\t{u.Name} - {u.Age}");
                }
            }
        }
    }
}