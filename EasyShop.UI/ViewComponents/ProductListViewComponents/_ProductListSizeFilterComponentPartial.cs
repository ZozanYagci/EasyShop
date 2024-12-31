using Core.Utilities.ApiClients;
using DTOs.DTOs.FilterAttributes;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.ViewComponents.ProductListViewComponents
{
    public class _ProductListSizeFilterComponentPartial:ViewComponent
    {

        private readonly ApiClient apiClient;

        public _ProductListSizeFilterComponentPartial(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sizes = await apiClient.GetAsync<List<SizeListDto>>("Filters/sizes");
            return View(sizes);
        }
    }
}
