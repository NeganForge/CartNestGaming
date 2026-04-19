using CartNestGaming.Data;
using CartNestGaming.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
public class UserController : Controller
{
    private readonly ApplicationDbContext _context;
    public UserController (ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult Index(string SearchString,String Category)
    {
        var Products = _context.Products
        .Include(p => p.Category)
                .AsQueryable();
        if (!String.IsNullOrEmpty(SearchString) )
        {
            Products = Products.Where(p => p.Name.ToLower().Contains(SearchString.ToLower()));
        }
        if (!String.IsNullOrEmpty(Category))
        {
            Products = Products.Where(P => P.Category.Name == Category);
        }
        ViewBag.Categories = _context.Categories.ToList();
        return View(Products.ToList());
    }
}