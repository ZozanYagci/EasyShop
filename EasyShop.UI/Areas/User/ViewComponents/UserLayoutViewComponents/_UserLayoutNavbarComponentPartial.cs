using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.Areas.User.ViewComponents.UserLayoutViewComponents
{
    public class _UserLayoutNavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
