using Core.Utilities.ApiClients;
using DTOs.DTOs.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailDescriptionComponentPartial:ViewComponent
    {
        private readonly ApiClient apiClient;

        public _ProductDetailDescriptionComponentPartial(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            var productDetail = await apiClient.GetAsync<List<ProductDetailsDto>>($"Products/ProductDetail/{productId}");
            return View(productDetail);
        }
    }
}
