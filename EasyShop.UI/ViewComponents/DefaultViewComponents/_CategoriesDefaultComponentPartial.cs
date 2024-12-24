using Core.Utilities.ApiClients;
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
        private readonly ApiClient apiClient;

        public _CategoriesDefaultComponentPartial(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new CategoriesViewModel();

            viewModel.Categories = await apiClient.GetAsync<List<CategoryListDto>>("Categories");

            viewModel.ProductStocks = await apiClient.GetAsync<List<ProductStockDto>>("Products/GetProductStocks");

            return View(viewModel);
        }

    }

}

