using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace smartdressroom.Controllers
{
    public class ConsultantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}