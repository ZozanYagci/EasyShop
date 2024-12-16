using Business.Abstract;
using DTOs.DTOs.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace EasyShop.UI.ViewComponents.ProductListViewComponents
{
    public class _ProductListComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductListComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessageRecentProduct = await client.GetAsync("https://localhost:44372/api/Products/");
            if (responseMessageRecentProduct.IsSuccessStatusCode)
            {
                var jsonData = await responseMessageRecentProduct.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ProductListDto>>(jsonData);
                return View(products);
            }
            return View();

        }
    }
}
