using CartNestGaming.Data;
using CartNestGaming.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CartNestGaming.Controllers.User
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString, string category)
        {
            // 🔐 SESSION CHECK (VERY IMPORTANT)
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Login", "AppUser");
            }

            var products = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            // 🔍 SEARCH
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p =>
                    p.Name.ToLower().Contains(searchString.ToLower()));
            }

            // 📂 CATEGORY FILTER
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category.Name == category);
            }

            // 📦 SEND DATA TO VIEW
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            // ✅ IMPORTANT PATH
            return View("~/Views/UserV/User/Index.cshtml", products.ToList());
        }
    }
}