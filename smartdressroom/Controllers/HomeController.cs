using System.Linq;
using Microsoft.AspNetCore.Mvc;
using smartdressroom.Models;
using smartdressroom.Storage;

namespace smartdressroom.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// База данных
        /// </summary>
        private readonly ApplicationContext _context;
  
        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        public HomeController(ApplicationContext context) => _context = context;

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Product(int code)
        {
            var m = _context.ClothesModels.Where(item => item.Code == code).FirstOrDefault();
            if (m != null)
            {
                ViewBag.Other = _context.ClothesModels
                    .Where(item => item.ID != m.ID && item.CollectionID == m.CollectionID).ToList();
                return View(m);
            }
            else
                return View(new ClothesModel(0, 0));
        }
    }
}