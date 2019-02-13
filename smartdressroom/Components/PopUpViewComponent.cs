using Microsoft.AspNetCore.Mvc;

namespace smartdressroom.Components
{
    public class PopUpViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
