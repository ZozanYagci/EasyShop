using Business.Abstract;
using Core.Utilities.ApiClients;
using DTOs.DTOs.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace EasyShop.UI.ViewComponents.ProductListViewComponents
{
    public class _ProductListComponentPartial : ViewComponent
    {
        private readonly ApiClient apiClient;
        public _ProductListComponentPartial(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await apiClient.GetAsync<List<ProductWithPricesDto>>("Products/ProductWithPrices");
            return View(products);
        }
    }
}
