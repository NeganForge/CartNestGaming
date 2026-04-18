using Microsoft.EntityFrameworkCore;
using CartNestGaming.Models;

namespace CartNestGaming.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 🔐 Users
        public DbSet<User> Users { get; set; }

        // 🎮 Products
        public DbSet<Product> Products { get; set; }

        // 📂 Categories
        public DbSet<Category> Categories { get; set; }

        // 🛒 Cart
        public DbSet<Cart> Carts { get; set; }

        // 📦 Orders
        public DbSet<Order> Orders { get; set; }

        // 📄 Order Items
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Payment> Payments { get; set; }

    }
}