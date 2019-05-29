using System.Linq;
using Microsoft.AspNetCore.Mvc;
using smartdressroom.Models;
using smartdressroom.Storage;

namespace smartdressroom.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        public HomeController() { }

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Product(int code)
        {
            ClothesModel m = null;
            using (var context = new ApplicationContext())
            {
                m = context.ClothesModels.Where(item => item.Code == code).FirstOrDefault();
            }
            if (m != null)
            {
                using (var context = new ApplicationContext())
                {
                    ViewBag.Other = context.ClothesModels
                        .Where(item => item.ID != m.ID && item.CollectionID == m.CollectionID).ToList();
                }
                return View(m);
            }
            else
                return View(new ClothesModel(0, 0));
        }
    }
}