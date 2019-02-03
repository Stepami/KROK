using System;
using System.Linq;
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
        /// Просмотр корзины
        /// </summary>
        public IActionResult Display(CartModel cart) => View(cart);

        [HttpPost]
        public IActionResult AddToCart(Guid id, int quantity, CartModel cart)
        {
            ClothesModel item = db.AppContext.ClothesModels.Where(a => a.ID == id).FirstOrDefault();
            if (item != null)
            {
                var ce = new CartItemModel(item, quantity);
                // Здесь была мистика - если напрямую использовать свойство cart, то
                // объект ce не добавлялся в список
                var c = cart;
                if (c.LineList.Exists(x => x.Item.ID == ce.Item.ID))
                    c.LineList[c.LineList.FindIndex(x => x.Item.ID == ce.Item.ID)].Quantity += quantity;
                else c.LineList.Add(ce);
                // Для сохранения в данных сессии
                string json = c.ToJson();
                HttpContext.Session.SetString("cart", json);
            }

            return RedirectToAction("Display", "Cart");
        }
    }
}
