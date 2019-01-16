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
                new ClothesModel(1, 132, 1000, "L", "SABBAT CULT", Url.Content("~/images/clothes/sabbat_tshirt1.png")),
            };
        }
        public IActionResult Index(ClothesModel Model)
        {
            Model = clothes[0];
            return View(Model);
        }
        public IActionResult Cart()
        {
            return View();
        }

        [HttpPost]
        public string Scan(int code)
        {
            if (clothes.Exists(item => item.Code == code))
            {
                var temp = clothes.Find(item => item.Code == code);
                return $"Price: {temp.Price} RUR\nSize: {temp.Size}\nBrand: {temp.Brand}";
            }
            else return "Scan_Error";
        }
    }
}