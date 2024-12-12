using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.ViewComponents.UILayoutViewComponents
{
    public class _ScriptComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
