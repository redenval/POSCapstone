using Microsoft.EntityFrameworkCore;

namespace Capstone.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ProductBaseImage> BaseImages { get; set; }
        public virtual DbSet<ProductPromoImage> PromoImages { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductOrder> ProductOrders { get; set; }
        public virtual DbSet<CartProduct> CartProducts { get; set; }
    }
}
