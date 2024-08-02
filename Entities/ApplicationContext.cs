using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Basket> Baskets { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=d:\mydb.db");
        }
    }
}
