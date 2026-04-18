using Microsoft.AspNetCore.Mvc;
using CartNestGaming.Data;
using CartNestGaming.Models;
using System;
using System.Linq;

public class PaymentController : Controller
{
    private readonly ApplicationDbContext _context;

    public PaymentController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var payments = _context.Payments.ToList();
        return View(payments);
    }
    public IActionResult Buy(int productId)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == productId);

        if (product == null)
            return NotFound();

        int userId = 1;

        var payment = new Payment
        {
            UserId = userId,
            ProductId = product.Id,
            Amount = product.Price,
            PaymentDate = DateTime.Now,
            Status = "Success"
        };

        _context.Payments.Add(payment);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}