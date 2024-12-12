using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.ViewComponents.UILayoutViewComponents
{
    public class _FooterComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
