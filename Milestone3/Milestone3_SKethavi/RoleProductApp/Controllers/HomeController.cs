using Microsoft.AspNetCore.Mvc;

namespace RoleProductApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}