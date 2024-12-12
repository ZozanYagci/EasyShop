using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.ViewComponents.UILayoutViewComponents
{
    public class _TopbarComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
