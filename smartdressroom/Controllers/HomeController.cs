using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using smartdressroom.Models;
using smartdressroom.Storage;

namespace smartdressroom.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// База данных
        /// </summary>
        ApplicationContext db;

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
        public HomeController() => db = new Storage.ApplicationContext();

        public IActionResult Index() => View();

        /// <summary>
        /// Просмотр корзины
        /// </summary>
        /// <returns></returns>
        public IActionResult Cart() => View(cart.LineList);

        [HttpPost]
        public IActionResult Product(int code)
        {
            var m = db.ClothesModels.Where(item => item.Code == code).FirstOrDefault();
            return m == null ? View(new ClothesModel(0, 0, "", "", "/images/scan_error.png")) : View(m);
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
                if (c.LineList.Exists(x => x.Item.ID == ce.Item.ID))
                    c.LineList[c.LineList.FindIndex(x => x.Item.ID == ce.Item.ID)].Quantity += 1;
                else c.LineList.Add(ce);
                // Для сохранения в данных сессии
                cart = c;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}