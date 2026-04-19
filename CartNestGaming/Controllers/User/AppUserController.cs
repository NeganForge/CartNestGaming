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

        // GET Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST Register


        // GET Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST Login
        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            var user = _context.AppUsers
                .FirstOrDefault(u => u.Email == Email && u.Password == Password);

            // ✅ FIXED CONDITION
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());

                return RedirectToAction("Index", "User"); // Store page
            }

            ViewBag.Error = "Invalid Login";
            return View();
        }

        // Protected page (optional here, better in UserController)
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Login");
            }

            return View();
        }
        [HttpPost]
        public IActionResult Register(AppUser user)
        {
            var existingUser = _context.AppUsers
                .FirstOrDefault(u => u.Email == user.Email);

            if (existingUser != null)
            {
                ViewBag.Error = "Email Already Exists";
                return View(user); // ✅ FIXED (not "user")
            }

            if (ModelState.IsValid)
            {
                _context.AppUsers.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(user);
        }
    }
}