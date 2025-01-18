using Core.Utilities.ApiClients;
using DTOs.DTOs.FilterDtos;
using EasyShop.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.ViewComponents.ProductListViewComponents
{
    public class _FilteredProductsComponentPartial : ViewComponent
    {

        private readonly ApiClient apiClient;

        public _FilteredProductsComponentPartial(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(FilterRequestDto filterRequest)
        {

            var products = await apiClient.PostAsync<List<ProductsDto>>("Filters/filtered-products", filterRequest);

            return View(products);
        }
    }
}
