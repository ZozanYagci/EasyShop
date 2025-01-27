using Business.Abstract;
using DTOs.DTOs.FilterDtos;
using DTOs.DTOs.ProductDtos;
using EasyShop.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

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

            ViewData["BreadCrumbPaths"] = new List<string> { "Ana Sayfa", "Ürünler", "Ürün Listesi" };
            return View();
        }
  
        public async Task<IActionResult> ProductDetail(int productId)
        {

            var productDetail = await productService.GetProductDetailsAsync(productId);

            ViewData["BreadCrumbPaths"] = new List<string> { "Ana Sayfa", "Ürünler", "Ürün Detay" };
            var model = new ProductDetailViewModel
            {
                ProductId = productId,
                ProductDetails = productDetail
                
            };
            return View(model);
        }
    }
}
