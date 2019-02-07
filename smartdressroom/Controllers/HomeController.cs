using System.Linq;
using Microsoft.AspNetCore.Mvc;
using smartdressroom.Models;
using smartdressroom.Services;

namespace smartdressroom.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// База данных
        /// </summary>
        private readonly IStorageService db;
  
        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        public HomeController(IStorageService ss) => db = ss;

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Product(int code)
        {
            var m = db.AppContext.ClothesModels.Where(item => item.Code == code).FirstOrDefault();
            return m == null ? View(new ClothesModel(0, 0, "", "", "/images/scan_error.png")) : View(m);
        }
    }
}