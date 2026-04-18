using Microsoft.AspNetCore.Mvc;

namespace CartNestGaming.Controllers
{
    public class AuthController : Controller
    {
        // SHOW LOGIN PAGE
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        // HANDLE LOGIN
        [HttpPost]
        public IActionResult AdminLogin(string username, string password)
        {
            if (username == "AdhiyanCNG" && password == "883850")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            ViewBag.Error = "Invalid Username or Password";
            return View(); // return same page with error
        }
        [HttpGet]
        public IActionResult UserLogin()
        {
            return RedirectToAction("Index", "User");
        }

    }   
}