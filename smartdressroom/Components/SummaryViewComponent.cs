using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smartdressroom.Models;

namespace smartdressroom.Components
{
    public class SummaryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            CartModel c = null;

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

            return View(c);
        }
    }
}
