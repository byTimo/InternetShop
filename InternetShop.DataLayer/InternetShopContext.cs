using System.Data.Entity;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer
{
    public class InternetShopContext : DbContext
    {
        internal static InternetShopContext Instance { get; } = new InternetShopContext();

        public InternetShopContext()
            : base("InternetShop")
        {
        }

        public virtual DbSet<OrderedProduct> OrderedProducts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Audio> Audios { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderedProducts)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Map<Audio>(p => p.Requires("ProductType").HasValue((byte)ProductType.Audio))
                .Map<Video>(p => p.Requires("ProductType").HasValue((byte)ProductType.Video));

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderedProducts)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
