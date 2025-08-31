using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoleProductApp.Models;
using System.Threading.Tasks;

namespace RoleProductApp.Data
{
    public class DbInitializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IDataProtectionProvider _provider;

        public DbInitializer(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext db, IDataProtectionProvider provider)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
            _provider = provider;
        }

        public async Task SeedAsync()
        {
            await _db.Database.MigrateAsync();

            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!await _roleManager.RoleExistsAsync("Manager"))
                await _roleManager.CreateAsync(new IdentityRole("Manager"));

            var admin = await _userManager.FindByNameAsync("admin");
            if (admin == null)
            {
                admin = new ApplicationUser { UserName = "admin", Email = "admin@example.com", EmailConfirmed = true };
                var result = await _userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            var manager = await _userManager.FindByNameAsync("manager1");
            if (manager == null)
            {
                manager = new ApplicationUser { UserName = "manager1", Email = "manager1@example.com", EmailConfirmed = true };
                var result = await _userManager.CreateAsync(manager, "Manager@123");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(manager, "Manager");
                }
            }

            if (!await _db.Products.AnyAsync())
            {
                var protector = _provider.CreateProtector("ProductPriceProtector");
                var p = new Product
                {
                    Name = "Sample Laptop",
                    ProtectedPrice = ApplicationDbContext.ProtectPrice(1200M, protector)
                };
                _db.Products.Add(p);
                await _db.SaveChangesAsync();
            }
        }
    }
}
