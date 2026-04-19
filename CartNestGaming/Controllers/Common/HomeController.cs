using CartNestGaming.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CartNestGaming.Controllers.Common
{
    public class HomeController: Controller
    {
        public IActionResult LoginChoice()
        {
            return View("~/Views/CommonV/Home/LoginChoice.cshtml");
        }

    }
    
}
