using System.Data.Entity;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer
{
    public class InternetShopContext : DbContext
    {
        public InternetShopContext()
            : base("name=InternetShop")
        {
        }

        public virtual DbSet<OrderedProduct> OrderedProducts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Audio> Audios { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderedProducts)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderedProducts)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("UsersRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
