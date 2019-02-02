using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smartdressroom.Models;
using smartdressroom.Services;

namespace smartdressroom.Controllers
{
    public class CartController : Controller
    {
        private readonly IStorageService db;

        public CartController(IStorageService ss) => db = ss;

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
        /// Просмотр корзины
        /// </summary>
        /// <returns></returns>
        public IActionResult Display() => View(cart);

        public IActionResult AddToCart(Guid id)
        {
            ClothesModel item = db.AppContext.ClothesModels.Where(a => a.ID == id).FirstOrDefault();
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

            return RedirectToAction("Display", "Cart");
        }
    }
}
