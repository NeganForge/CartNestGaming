using CartNestGaming.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CartNestGaming.Controllers
{
    public class HomeController: Controller
    {
        public IActionResult LoginChoice()
        {
            return View();
        }

    }
    
}
