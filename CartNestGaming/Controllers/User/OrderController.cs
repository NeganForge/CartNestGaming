using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CartNestGaming.Data;
using CartNestGaming.Models;
using System;
using System.Linq;

namespace CartNestGaming.Controllers.User
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔥 CHECKOUT
        public IActionResult Checkout()
        {
            int userId = 1; // TEMP USER (replace later with login user)

            var cartItems = _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToList();

            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            decimal total = cartItems.Sum(c => c.Product.Price * c.Quantity);

            Order order = new Order()
            {
                UserId = userId,
                OrderCode = "ORD" + DateTime.Now.Ticks,
                TotalAmount = total,
                OrderDate = DateTime.Now,
                OrderItems = cartItems.Select(c => new OrderItem
                {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    Price = c.Product.Price
                }).ToList()
            };

            _context.Orders.Add(order);

            // ✅ Clear cart after order
            _context.Carts.RemoveRange(cartItems);

            _context.SaveChanges();

            return RedirectToAction("Success");
        }

        // ✅ SUCCESS PAGE
        public IActionResult Success()
        {
            return View("~/Views/UserV/Order/Success.cshtml");
        }

        // 📦 USER ORDER HISTORY
        public IActionResult MyOrders()
        {
            int userId = 1; // SAME TEMP USER

            var orders = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View("~/Views/UserV/Order/MyOrders.cshtml", orders);
        }
    }
}