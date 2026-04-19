using Microsoft.AspNetCore.Mvc;

namespace CartNestGaming.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View("~/Views/AdminV/Admin/Dashboard.cshtml");
        }
    }
}
