using DTOs.DTOs.SubCategoryDtos;
using EasyShop.DTOs.DTOs.CategoryDtos;
using EasyShop.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EasyShop.UI.ViewComponents.UILayoutViewComponents
{
    public class _NavbarComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        public _NavbarComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new NavbarViewModel();
            var client = _httpClientFactory.CreateClient();
            var responseMessageCategories = await client.GetAsync("https://localhost:44372/api/Categories");
            if (responseMessageCategories.IsSuccessStatusCode)
            {
                var jsonData = await responseMessageCategories.Content.ReadAsStringAsync();
               viewModel.Categories = JsonConvert.DeserializeObject<List<CategoryListDto>>(jsonData);
               
            }

            var responseMessageSubCategories = await client.GetAsync("https://localhost:44372/api/SubCategories/GetSubCategoryByCategoryId");
            if (responseMessageSubCategories.IsSuccessStatusCode)
            {
                var jsonData2= await responseMessageSubCategories.Content.ReadAsStringAsync();
                viewModel.SubCategories = JsonConvert.DeserializeObject<List<GetSubCategoryByCategoryIdDto>>(jsonData2);
   
            }

            return View(viewModel);
        }
    }
}
