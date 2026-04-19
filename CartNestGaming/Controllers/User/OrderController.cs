using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CartNestGaming.Data;
using CartNestGaming.Models;
using System;
using System.Linq;
using CartNestGaming.Models;

namespace CartNestGaming.Controllers.User
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController (ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Checkout()
        {
            string userId = "guest";

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

            // ✅ Clear Cart after order
            _context.Carts.RemoveRange(cartItems);

            _context.SaveChanges();

            return RedirectToAction("Success");
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult MyOrders()
        {
            String UserId = "guest";
            var orders = _context.Orders
               .Include(o => o.OrderItems)
               .ThenInclude(oi => oi.Product)
               .Where(o => o.UserId == UserId)
               .OrderByDescending(o => o.OrderDate)
               .ToList();
            return View(orders);
        }
    }
}