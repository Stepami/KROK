using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using smartdressroom.Models;

namespace smartdressroom.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// База данных
        /// </summary>
        Storage.Database db;

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
            db = new Storage.Database();
        }

        public IActionResult Index() => View();

        /// <summary>
        /// Просмотр корзины
        /// </summary>
        /// <returns></returns>
        public IActionResult Cart() => View(cart.List);

        [HttpPost]
        public IActionResult Product(int code)
        {
            ClothesModel m = db.ClothesModels.Where(item => item.Code == code).FirstOrDefault();
            if (m == null)
            {
                m = new ClothesModel(0, 0, "", "", "/images/scan_error.png");
            }
            return View(m);
        }

        public IActionResult AddToCart(Guid id)
        {
            ClothesModel item = db.ClothesModels.Where(a => a.ID == id).FirstOrDefault();
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