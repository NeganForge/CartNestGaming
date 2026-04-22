using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CartNestGaming.Data;
using System.Linq;
using CartNestGaming.Models;

namespace CartNestGaming.Controllers.User
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ 1. View Cart
        public IActionResult Index()
        {
            int userId = 1; // TEMP USER (must be int)

            var cartItems = _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToList();

            return View("~/Views/UserV/Cart/Index.cshtml", cartItems);
        }

        // ✅ 2. Add to Cart
        public IActionResult AddToCart(int productId)
        {
            int userId = 1; // TEMP USER

            var existingItem = _context.Carts
                .FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                Cart cart = new Cart()
                {
                    ProductId = productId,
                    Quantity = 1,
                    UserId = userId
                };

                _context.Carts.Add(cart);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // ✅ 3. Increase Quantity
        public IActionResult Increase(int id)
        {
            var item = _context.Carts.Find(id);

            if (item != null)
            {
                item.Quantity += 1;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // ✅ 4. Decrease Quantity
        public IActionResult Decrease(int id)
        {
            var item = _context.Carts.Find(id);

            if (item != null && item.Quantity > 1)
            {
                item.Quantity -= 1;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // ✅ 5. Remove Item
        public IActionResult Remove(int id)
        {
            var item = _context.Carts.Find(id);

            if (item != null)
            {
                _context.Carts.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}