using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.ViewComponents.DefaultViewComponents
{
    public class _CarouselDefaultComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
