using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smartdressroom.Models;
using smartdressroom.Storage;

namespace smartdressroom.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        public HomeController() { }

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Product(string vcode)
        {
            ClothesModel m = null;
            using (var context = new ApplicationContext())
            {
                m = context.ClothesModels.Include(cm => cm.Collection.ClothesModels)
                    .Where(item => item.VendorCode == vcode).FirstOrDefault();
            }
            if (m != null)
            {
                m.SelectedSize = m.Sizes[0];
                return PartialView(m);
            }
            else
                return PartialView(new ClothesModel("0", 0));
        }

        [HttpGet]
        public IActionResult ProductSizes(string vcode)
        {
            string[] s = null;
            using (var context = new ApplicationContext())
            {
                s = context.ClothesModels.Include(cm => cm.Collection.ClothesModels)
                    .Where(item => item.VendorCode == vcode).FirstOrDefault().Sizes;
            }
            if (s != null)
                return PartialView(s);
            else
                return PartialView(new string[0]);
        }
    }
}