using Microsoft.AspNetCore.Mvc;

namespace smartdressroom.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
    }
}