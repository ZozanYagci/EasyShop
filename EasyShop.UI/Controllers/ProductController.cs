using EasyShop.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "Product";

            var model = new BreadCrumbViewModel
            {
                Paths = new List<string> { "Ana Sayfa", "Ürünler", "Ürün Listesi" }
            };

            return View(model);
        }
    }
}
