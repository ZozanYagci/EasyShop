using DTOs.DTOs.ProductDtos;
using EasyShop.DTOs.DTOs.CategoryDtos;
using EasyShop.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace EasyShop.UI.ViewComponents.DefaultViewComponents
{
    public class _CategoriesDefaultComponentPartial : ViewComponent
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public _CategoriesDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new CategoriesViewModel();

            var client = _httpClientFactory.CreateClient();
            var responseMessageCategories = await client.GetAsync("https://localhost:44372/api/Categories");
            if (responseMessageCategories.IsSuccessStatusCode)
            {
                var jsonData = await responseMessageCategories.Content.ReadAsStringAsync();
                 viewModel.Categories = JsonConvert.DeserializeObject<List<CategoryListDto>>(jsonData);
                
            }

            var responseMessageProductStock = await client.GetAsync("https://localhost:44372/api/Products/GetProductStocks");
            if (responseMessageProductStock.IsSuccessStatusCode)
            {
                var jsonData = await responseMessageProductStock.Content.ReadAsStringAsync();
                viewModel.ProductStocks = JsonConvert.DeserializeObject<List<ProductStockDto>>(jsonData);

            }
            return View(viewModel);
        }
           
        }

    }

