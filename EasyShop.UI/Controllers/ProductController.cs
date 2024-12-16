using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "Product";
            return View();
        }
    }
}
