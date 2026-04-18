using CartNestGaming.Data;
using CartNestGaming.Models;
using Microsoft.AspNetCore.Mvc;

namespace CartNestGaming.Controllers
{
   public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
    }
}
