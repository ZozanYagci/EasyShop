using Core.Utilities.ApiClients;
using DTOs.DTOs.FilterDtos;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.ViewComponents.ProductListViewComponents
{
    public class _ProductFilterValuesComponentPartial : ViewComponent
    {

        private readonly ApiClient apiClient;

        public _ProductFilterValuesComponentPartial(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var filterValues = await apiClient.GetAsync<List<FilterValueDto>>("Filters/filter-values");
                return View(filterValues);
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message}");
               
            }
        }
    }
}
