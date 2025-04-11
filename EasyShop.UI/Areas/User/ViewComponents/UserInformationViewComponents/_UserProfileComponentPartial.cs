using Core.Utilities.ApiClients;
using DTOs.DTOs.ProductDtos;
using DTOs.DTOs.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.Areas.User.ViewComponents.UserInformationViewComponents
{
    public class _UserProfileComponentPartial:ViewComponent
    {
        private readonly ApiClient _apiClient;

        public _UserProfileComponentPartial(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var user = await _apiClient.GetAsync<UserProfileUpdateDto>("User/my-profile");

            return View(user);
            
        }
    }
}
