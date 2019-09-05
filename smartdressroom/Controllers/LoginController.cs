using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using smartdressroom.Storage;

namespace smartdressroom.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationContext _context;

        public LoginController(ApplicationContext _context) => this._context = _context;

        public IActionResult AdminPanel() => View();

        public IActionResult AdminLogin() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdminLogin(string login, string password)
        {
            var admin = _context.Admins
                .Where(a => a.Login == login && a.Password == password)
                .FirstOrDefault();
            if (admin != null)
            {
                HttpContext.Session.SetString(nameof(admin), JsonConvert.SerializeObject(admin));
                return RedirectToAction("AdminPanel");
            }
            else return View();
        }

        public IActionResult AdminLogout()
        {
            HttpContext.Session.Remove("admin");
            return RedirectToAction("AdminLogin");
        }
    }
}