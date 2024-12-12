using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
