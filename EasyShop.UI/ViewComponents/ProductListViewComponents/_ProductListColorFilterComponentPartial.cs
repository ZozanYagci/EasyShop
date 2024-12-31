using Core.Utilities.ApiClients;
using DTOs.DTOs.FilterAttributes;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.ViewComponents.ProductListViewComponents
{
    public class _ProductListColorFilterComponentPartial:ViewComponent
    {

        private readonly ApiClient apiClient;

        public _ProductListColorFilterComponentPartial(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var colors = await apiClient.GetAsync<List<ColorListDto>>("Filters/colors");
                return View(colors);
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message}");
               
            }
        }
    }
}
