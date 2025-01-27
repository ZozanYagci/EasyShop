using EasyShop.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.ViewComponents.UILayoutViewComponents
{
    public class _NavigationPathComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<string> paths)
        {
        
            return View(paths);
        }
    }
}
