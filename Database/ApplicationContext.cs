using Microsoft.EntityFrameworkCore;
using Models;
using System.Diagnostics;

namespace Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Basket> Baskets { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<BasketProduct> BasketsProducts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies()
                .UseSqlite(@"Data Source=d:\mydb.db")
                .LogTo(s => Debug.WriteLine(s));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasketProduct>().HasKey(bp => new { bp.BasketId, bp.ProductId });
        }
    }
}
