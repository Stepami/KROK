using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using smartdressroom.Models;

namespace smartdressroom.Controllers
{
    public class HomeController : Controller
    {
        List<ClothesModel> clothes;

        /// <summary>
        /// Корзина, сохраняемая в сессии
        /// </summary>
        CartModel cart
        {
            get
            {
                CartModel c;              
                if (HttpContext.Session.Keys.Contains("cart"))
                {
                    string json = HttpContext.Session.GetString("cart");
                    c = CartModel.FromJson(json);
                }
                else
                {
                    c = new CartModel();
                    string json = c.ToJson();
                    HttpContext.Session.SetString("cart", json);
                }
                return c;
            }
            set
            {
                string json = value.ToJson();
                HttpContext.Session.SetString("cart", json);
            }
        }

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        public HomeController()
        {
            clothes = new List<ClothesModel>
            {
                new ClothesModel(1, 132, 1000, "L", "SABBAT CULT", "/images/clothes/sabbat_tshirt1.jpg"),
                new ClothesModel(2, 12, 1200, "L", "SELA", "/images/clothes/sela_jemper1.jpg")
            };
            
        }
        public IActionResult Index() => View();

        /// <summary>
        /// Просмотр корзины
        /// </summary>
        /// <returns></returns>
        public IActionResult Cart() => View(cart.List);

        [HttpPost]
        public IActionResult Product(int code) => clothes.Exists(item => item.Code == code)
            ? View(clothes.Find(item => item.Code == code))
            : View(new ClothesModel(0, 0, 0, "", "", "/images/scan_error.png"));

        public IActionResult AddToCart(int id)
        {
            ClothesModel item = clothes.Find(x => x.Id == id);
            if (item != null)
            {
                var ce = new CartItemModel(item, 1);
                // Здесь была мистика - если напрямую использовать свойство cart, то
                // объект ce не добавлялся в список
                var c = cart;
                c.List.Add(ce);
                // Для сохранения в данных сессии
                cart = c;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}