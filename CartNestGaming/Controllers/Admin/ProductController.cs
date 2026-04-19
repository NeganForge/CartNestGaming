    using CartNestGaming.Data;
    using CartNestGaming.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;


    namespace CartNestGaming.Controllers.Admin
    {
        public class ProductController : Controller
        {
            private readonly ApplicationDbContext _context;
            public ProductController(ApplicationDbContext context)
            {
                _context = context;
            }
            public IActionResult Index()
            {
                var products = _context.Products
                              .Include(p => p.Category)
                              .ToList();

            return View("~/Views/AdminV/Product/Index.cshtml", products);
        }

            [HttpPost]
            public IActionResult Create(Product product)
            {
                if (ModelState.IsValid)
                {
                    _context.Products.Add(product);
                    _context.SaveChanges();
                return RedirectToAction("Index");
            }

                // 🔥 IMPORTANT: reload categories
                ViewBag.Categories = _context.Categories.ToList();

                return View(product);
            }
            public IActionResult SeedCategory()
            {
                _context.Categories.Add(new Category { Name = "Action" });
                _context.Categories.Add(new Category { Name = "RPG" });
                _context.Categories.Add(new Category { Name = "Sports" });

                _context.SaveChanges();

                return Content("Categories Added");
            }
            public IActionResult Details(int id)
            {
                var product = _context.Products
                    .Include(p => p.Category)
                    .FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return NotFound();
                }
                return View("~/Views/AdminV/Product/Details.cshtml" ,product);
            }
            public ActionResult Edit(int id)
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound();
                }
                ViewBag.Categories = _context.Categories.ToList();
                return View("~/Views/AdminV/Product/Edit.cshtml" ,product);
            }
            public IActionResult Create()
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View("~/Views/AdminV/Product/Create.cshtml");
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(Product product)
            {
                if (ModelState.IsValid)
                {
                    _context.Products.Update(product);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Categories = _context.Categories.ToList();
                return View(product);
            }
            public IActionResult Delete(int id)
            {
                var product = _context.Products
                    .Include(p => p.Category)
                    .FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    return NotFound();
                }

                return View("~/Views/AdminV/Product/Delete.cshtml" ,product);
            }
            [HttpPost, ActionName("Delete")]

            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(int id)
            {
                var product = _context.Products.Find(id);

                if (product == null)
                {
                    return NotFound();
                }

                _context.Products.Remove(product);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            public IActionResult ByCategory(int id)
            {
                var products = _context.Products
                    .Where(p => p.CategoryId == id)
                        .ToList();
                return View("ByCategory",products);
            }
        
        }
    }
