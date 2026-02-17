using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ecommerce.Models;

namespace ecommerce.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        // --- Store Management ---
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        // --- Content ---
        public DbSet<BlogPost> BlogPosts { get; set; }


        // --- Customer Experience ---
        // REMOVED: CartItems (We use Session for this!)
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Review> Reviews { get; set; }

        // --- Order Processing ---
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Tell EF to explicitly ignore CartItem so it doesn't crash migrations
            builder.Ignore<CartItem>();

            // Setting precision for currency values
            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            builder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            builder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasPrecision(18, 2);
        }
    }
}