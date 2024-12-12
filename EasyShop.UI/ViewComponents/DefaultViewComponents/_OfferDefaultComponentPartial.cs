using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.ViewComponents.DefaultViewComponents
{
    public class _OfferDefaultComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
