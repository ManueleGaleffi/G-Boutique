using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ecommerce.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace ecommerce.Areas.Identity.Data
{
    public class ecommerceIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public ecommerceIdentityDbContext(DbContextOptions<ecommerceIdentityDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .Ignore(u => u.EmailConfirmed)
                .Ignore(u => u.PhoneNumber)
                .Ignore(u => u.PhoneNumberConfirmed)
                .Ignore(u => u.TwoFactorEnabled)
                .Ignore(u => u.LockoutEnabled)
                .Ignore(u => u.LockoutEnd)
                .Ignore(u => u.AccessFailedCount)
                .Property(e => e.Id).HasColumnName("UserId");

            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserToken");

            // Configurazione per l'entità Cart
            modelBuilder.Entity<Cart>()
                .HasKey(c => c.CartId); // Chiave primaria
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId) // Chiave esterna per ProductId
                .OnDelete(DeleteBehavior.Restrict); // Rimuovi questa riga se vuoi che l'eliminazione del prodotto non influenzi il carrello
        }
    }
}
