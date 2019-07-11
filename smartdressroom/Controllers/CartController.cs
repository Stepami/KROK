using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smartdressroom.Models;
using smartdressroom.Services;
using smartdressroom.Storage;

namespace smartdressroom.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService) => this.cartService = cartService;
        
        /// <summary>
        /// Просмотр корзины
        /// </summary>
        public IActionResult Display() => View(cartService.GetCart(HttpContext.Session));

        [HttpPost]
        public IActionResult AddToCart(Guid id, int quantity)
        {
            ClothesModel item = null;
            using (var context = new ApplicationContext())
            {
                item = context.ClothesModels.Where(a => a.ID == id).FirstOrDefault();
            }
            if (item != null)
            {
                var ce = new CartItemModel(item, quantity);
                var c = cartService.GetCart(HttpContext.Session);

                if (c.LineList.Exists(x => x.Item.ID == ce.Item.ID))
                    c.LineList[c.LineList.FindIndex(x => x.Item.ID == ce.Item.ID)].Quantity += quantity;
                else c.LineList.Add(ce);

                cartService.SetCart(c.ToJson(), HttpContext.Session);
            }

            return RedirectToAction("Display", "Cart");
        }

        public IActionResult RemoveFromCart(Guid id)
        {
            var c = cartService.GetCart(HttpContext.Session);
            CartItemModel item = c.LineList.FirstOrDefault(x => x.Item.ID == id);

            if (item != null)
                c.RemoveLine(item);

            cartService.SetCart(c.ToJson(), HttpContext.Session);

            return RedirectToAction("Display");
        }

        public IActionResult ClearCart()
        {
            cartService.ClearCart(HttpContext.Session);

            return RedirectToAction("Index", "Home");
        }
    }
}
