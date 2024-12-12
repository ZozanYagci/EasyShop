using DTOs.DTOs.ProductDtos;
using EasyShop.DTOs.DTOs.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EasyShop.UI.ViewComponents.DefaultViewComponents
{
    public class _RecentProductDefaultComponentPartial : ViewComponent
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public _RecentProductDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessageRecentProduct = await client.GetAsync("https://localhost:44372/api/Products/GetRecentProduct");
            if (responseMessageRecentProduct.IsSuccessStatusCode)
            {
                var jsonData = await responseMessageRecentProduct.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<RecentProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
