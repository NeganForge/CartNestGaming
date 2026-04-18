using Microsoft.AspNetCore.Mvc;

namespace CartNestGaming.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
