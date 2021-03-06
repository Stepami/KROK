﻿using System;
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
        private readonly ApplicationContext _context;

        public CartController(ICartService cartService, ApplicationContext _context)
        {
            this.cartService = cartService;
            this._context = _context;
        }

        public IActionResult Display() => PartialView(cartService.GetCart(HttpContext.Session));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddToCart(Guid id, int quantity, string selectedSize)
        {
            var item = _context.ClothesModels.Where(a => a.ID == id).FirstOrDefault();
            if (item != null)
            {
                item.SelectedSize = selectedSize;
                var ce = new CartItemModel(item, quantity);
                var c = cartService.GetCart(HttpContext.Session);

                if (c.LineList.Exists(x => x.Item.ID == ce.Item.ID && x.Item.SelectedSize == ce.Item.SelectedSize))
                    c.LineList[c.LineList.FindIndex(x => x.Item.ID == ce.Item.ID)].Quantity += quantity;
                else c.LineList.Add(ce);

                cartService.SetCart(c, HttpContext.Session);
            }

            return Json(Url.Action("Display", "Cart"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RemoveFromCart(Guid id)
        {
            var c = cartService.GetCart(HttpContext.Session);
            CartItemModel item = c.LineList.FirstOrDefault(x => x.Item.ID == id);

            if (item != null)
                c.RemoveLine(item);

            cartService.SetCart(c, HttpContext.Session);

            return Json(Url.Action("Display", "Cart"));
        }

        public IActionResult ClearCart()
        {
            cartService.ClearCart(HttpContext.Session);

            return RedirectToAction("Index", "Home");
        }
    }
}
