using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using smartdressroom.Models;

namespace smartdressroom.Controllers
{
    public class CartController : Controller
    {
        public List<ClothesModel> cart = new List<ClothesModel>();

        public IActionResult Cart() => View(cart);

        [HttpPost]
        public void AddToCart(int id, int code, int price, string size, string brand, string path) => cart.Add(new ClothesModel(id, code, price, size, brand, path));
    }
}
