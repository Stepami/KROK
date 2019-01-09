using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using smartdressroom.Models;

namespace smartdressroom.Controllers
{
    public class HomeController : Controller
    {
        private List<ClothesModel> clothes;

        public HomeController()
        {
            clothes = new List<ClothesModel>
            {
                new ClothesModel(1, 132, 1000, "L", "SABBAT CULT"),
                new ClothesModel(2, 1456, 500, "M", "SELA")
            };
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }

        [HttpPost]
        public string Scan(int code) => clothes.Exists(item => item.Code == code) ? "yes" : "no";
    }
}