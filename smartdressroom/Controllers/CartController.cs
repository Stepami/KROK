using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smartdressroom.Models;
using smartdressroom.Storage;

namespace smartdressroom.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationContext _context;

        public CartController(ApplicationContext context) => _context = context;
        
        /// <summary>
        /// Просмотр корзины
        /// </summary>
        public IActionResult Display(CartModel cart) => View(cart);

        [HttpPost]
        public IActionResult AddToCart(Guid id, int quantity, CartModel cart)
        {
            ClothesModel item = _context.ClothesModels.Where(a => a.ID == id).FirstOrDefault();
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
