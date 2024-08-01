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
                orch.Delete();
                orch.Create(new User() { Name = "FromConsole", Age = 30 });
                var users = orch.Read();
                Console.WriteLine("Users list:");
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}\t{u.Name} - {u.Age}");
                }
            }
        }
    }
}