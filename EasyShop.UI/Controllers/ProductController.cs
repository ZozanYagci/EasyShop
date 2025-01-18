using DTOs.DTOs.FilterDtos;
using EasyShop.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "Product";

            var filterRequest = new FilterRequestDto
            {
                Colors = new List<string>(),
                Components = new List<string>(),
                Sizes = new List<string>(),
                MinPrice = null,
                MaxPrice = null
            };

            ViewData["FilterRequest"] = filterRequest;

            var model = new BreadCrumbViewModel
            {
                Paths = new List<string> { "Ana Sayfa", "Ürünler", "Ürün Listesi" }
            };

            return View(model);
        }
    }
}
