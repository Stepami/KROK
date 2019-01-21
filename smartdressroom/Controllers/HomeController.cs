using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using smartdressroom.Models;

namespace smartdressroom.Controllers
{
    public class HomeController : Controller
    {
        List<ClothesModel> clothes;
        List<CartModel> cart = new List<CartModel>();

        public HomeController() => clothes = new List<ClothesModel>
            {
                new ClothesModel(1, 132, 1000, "L", "SABBAT CULT", "/images/clothes/sabbat_tshirt1.jpg"),
                new ClothesModel(2, 12, 1200, "L", "SELA", "/images/clothes/sela_jemper1.jpg")
            };

        public IActionResult Index() => View();
        public IActionResult Cart() => View(cart);

        [HttpPost]
        public IActionResult Product(int code) => clothes.Exists(item => item.Code == code)
            ? View(clothes.Find(item => item.Code == code))
            : View(new ClothesModel(0, 0, 0, "", "", "/images/scan_error.png"));

        public IActionResult AddToCart(int id)
        {
            ClothesModel item = clothes.Find(x => x.Id == id);
            if (item != null)
            {
                var ce = new CartModel(item, 1);
                cart.Add(ce);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}