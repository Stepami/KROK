using Microsoft.AspNetCore.Mvc;

namespace smartdressroom.Components
{
    public class RebootViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
