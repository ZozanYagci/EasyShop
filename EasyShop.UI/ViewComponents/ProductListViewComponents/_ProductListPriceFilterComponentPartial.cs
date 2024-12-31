using Core.Utilities.ApiClients;
using DTOs.DTOs.FilterAttributes;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.ViewComponents.ProductListViewComponents
{
    public class _ProductListPriceFilterComponentPartial:ViewComponent
    {

        private readonly ApiClient apiClient;

        public _ProductListPriceFilterComponentPartial(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var priceRange = await apiClient.GetAsync<List<PriceRangeListDto>>("Filters/price-range");
            return View(priceRange);
        }
    }
}
