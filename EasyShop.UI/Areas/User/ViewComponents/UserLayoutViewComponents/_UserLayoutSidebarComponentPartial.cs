using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.Areas.User.ViewComponents.UserLayoutViewComponents
{
    public class _UserLayoutSidebarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
