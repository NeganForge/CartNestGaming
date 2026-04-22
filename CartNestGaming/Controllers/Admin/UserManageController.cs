using CartNestGaming.Data;
using Microsoft.AspNetCore.Mvc;

namespace CartNestGaming.Controllers.Admin
{
    public class UserManageController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserManageController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Indexx()
        {
            var res = _context.AppUsers.ToList();
            return View("~/Views/AdminV/OandUTrack/Indexx.cshtml",res);
        }
    }
}
