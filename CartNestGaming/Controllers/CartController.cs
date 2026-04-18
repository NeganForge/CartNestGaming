using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CartNestGaming.Data;
using CartNestGaming.Models;
using System.Linq;

namespace CartNestGaming.Controllers
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
            string userId = "guest"; // later replace with logged-in user

            var cartItems = _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToList();

            return View(cartItems);
        }

        // ✅ 2. Add to Cart
        public IActionResult AddToCart(int productId)
        {
            string userId = "guest";

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