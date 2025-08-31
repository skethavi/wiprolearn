using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoleProductApp.Models;

namespace RoleProductApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IDataProtector _protector;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDataProtectionProvider provider)
            : base(options)
        {
            _protector = provider.CreateProtector("ProductPriceProtector");
        }

        public DbSet<Product> Products { get; set; }

        public static decimal UnprotectPrice(string protectedPrice, IDataProtector protector)
        {
            if (string.IsNullOrEmpty(protectedPrice)) return 0M;
            var unprotected = protector.Unprotect(protectedPrice);
            return decimal.Parse(unprotected);
        }

        public static string ProtectPrice(decimal price, IDataProtector protector)
        {
            return protector.Protect(price.ToString());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>().Property(p => p.ProtectedPrice).IsRequired();
        }
    }
}
