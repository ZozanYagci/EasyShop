using Core.Utilities.ApiClients;
using DTOs.DTOs.ProductDtos;
using EasyShop.DTOs.DTOs.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EasyShop.UI.ViewComponents.DefaultViewComponents
{
    public class _RecentProductDefaultComponentPartial : ViewComponent
    {

        private readonly ApiClient apiClient;

        public _RecentProductDefaultComponentPartial(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var recentProducts = await apiClient.GetAsync<List<RecentProductDto>>("Products/GetRecentProduct");

            return View(recentProducts);

        }
    }
}
