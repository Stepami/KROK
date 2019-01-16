using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using smartdressroom.Models;

namespace smartdressroom.Controllers
{
    public class HomeController : Controller
    {
        public List<ClothesModel> clothes;

        public HomeController()
        {
            clothes = new List<ClothesModel>
            {
                new ClothesModel(1, 132, 1000, "L", "SABBAT CULT", "/images/clothes/sabbat_tshirt1.jpg"),
                new ClothesModel(2, 12, 1200, "L", "SELA", "/images/clothes/sela_jemper1.jpg")
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
        public IActionResult Product(int code)
        {
            if (clothes.Exists(item => item.Code == code))
            {
                var temp = clothes.Find(item => item.Code == code);
                return View(temp);
            }
            else return Content("Scan_Error");
        }
    }
}