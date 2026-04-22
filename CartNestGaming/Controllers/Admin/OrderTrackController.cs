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
                .Include(p => p.Product)
                .ThenInclude(p => p.Category)
                .GroupBy(p => p.Product.Category.Name)
                .Select(n => new
                {
                    CategoryName = n.Key,
                    TotalSold = n.Sum(x => x.Quantity)
                }).ToList();
            return View("~/Views/AdminV/OandUTRack/Index.cshtml", res);
        }
    }
}
