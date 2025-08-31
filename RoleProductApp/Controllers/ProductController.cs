using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleProductApp.Data;
using RoleProductApp.Models;
using RoleProductApp.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace RoleProductApp.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IDataProtector _protector;

        public ProductController(ApplicationDbContext db, IDataProtectionProvider provider)
        {
            _db = db;
            _protector = provider.CreateProtector("ProductPriceProtector");
        }

        public async Task<IActionResult> Index()
        {
            var products = await _db.Products.ToListAsync();
            var vm = products.Select(p => new ProductListItemViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = ApplicationDbContext.UnprotectPrice(p.ProtectedPrice, _protector)
            }).ToList();

            return View("ProductList", vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View("CreateProduct");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (!ModelState.IsValid) return View("CreateProduct", model);

            var product = new Product
            {
                Name = model.Name,
                ProtectedPrice = ApplicationDbContext.ProtectPrice(model.Price, _protector)
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            TempData["Success"] = $"Product \"{product.Name}\" has been successfully created!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var p = await _db.Products.FindAsync(id);
            if (p == null) return NotFound();

            var vm = new EditProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = ApplicationDbContext.UnprotectPrice(p.ProtectedPrice, _protector)
            };
            return View("EditProduct", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            if (!ModelState.IsValid) return View("EditProduct", model);

            var p = await _db.Products.FindAsync(model.Id);
            if (p == null) return NotFound();

            p.Name = model.Name;
            p.ProtectedPrice = ApplicationDbContext.ProtectPrice(model.Price, _protector);
            p.UpdatedAt = System.DateTime.UtcNow;

            _db.Products.Update(p);
            await _db.SaveChangesAsync();

            TempData["Success"] = $"Product \"{p.Name}\" has been successfully updated!";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Products.FindAsync(id);
            if (p == null) return NotFound();

            _db.Products.Remove(p);
            await _db.SaveChangesAsync();

            TempData["Success"] = $"Product \"{p.Name}\" has been successfully deleted!";
            return RedirectToAction("Index");
        }
    }
}
