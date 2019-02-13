using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace smartdressroom.Components
{
    public class CarouselViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
