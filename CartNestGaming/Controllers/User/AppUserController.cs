using CartNestGaming.Data;
using CartNestGaming.Models;
using Microsoft.AspNetCore.Mvc;

namespace CartNestGaming.Controllers.User
{
    public class AppUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================= REGISTER =================

        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/UserV/AppUser/Register.cshtml");
        }

        [HttpPost]
        public IActionResult Register(AppUser user)
        {
            var existingUser = _context.AppUsers
                .FirstOrDefault(u => u.Email == user.Email);

            if (existingUser != null)
            {
                ViewBag.Error = "Email already exists";
                return View("~/Views/UserV/AppUser/Register.cshtml", user);
            }

            if (ModelState.IsValid)
            {
                _context.AppUsers.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View("~/Views/UserV/AppUser/Register.cshtml", user);
        }

        // ================= LOGIN =================

        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/UserV/AppUser/Login.cshtml");
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            var user = _context.AppUsers
                .FirstOrDefault(u => u.Email == Email && u.Password == Password);

            if (user != null)
            {
                // ✅ Save session
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserName", user.Name);

                // ✅ Go to store
                return RedirectToAction("Index", "User");
            }

            ViewBag.Error = "Invalid Email or Password";
            return View("~/Views/UserV/AppUser/Login.cshtml");
        }

        // ================= LOGOUT =================

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}