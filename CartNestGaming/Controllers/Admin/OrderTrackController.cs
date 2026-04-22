using CartNestGaming.Data;
using CartNestGaming.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace CartNestGaming.Controllers.Admin
{
    public class OrderTrackController : Controller
    {
       private readonly ApplicationDbContext _context;
        public OrderTrackController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("AdminV/OrderTrack")]
        public IActionResult Index()
        {
            var res = _context.OrderItems
                .Include(n => n.Product)
                .ThenInclude(c => c.Category)
                 .GroupBy(n => n.Product.Category.Name)
                 .Select(n => new
                 {
                     Category = n.Key,
                     TotalOrder = n.Count()
                 }).ToList();
            return View("~/Views/AdminV/OrderTrac/Index.cshtml",res);
                 
        }
    }
}
