using Core.Extensions;
using Core.Utilities.ApiClients;
using Core.Utilities.Results;
using DTOs.DTOs.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    public class ProfileController : Controller
    {
        private readonly ApiClient _apiClient;

        public ProfileController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserProfileUpdateDto model)
        {

            if (!ModelState.IsValid)
            {
                var valErrors = ModelState
                   .Where(x => x.Value.Errors.Any())
                   .ToDictionary(
                      e => e.Key,
                      e => e.Value.Errors.Select(x => x.ErrorMessage).ToArray()
           );

                return BadRequest(new { errors = valErrors });
            }

            var response = await _apiClient.PutAsync<ServiceResponse<object>>("User/update-profile", model);

            if (!response.Success)
            {
            return BadRequest(new { success = false, message = "Güncelleme başarısız oldu, lütfen tekrar deneyin." });
           }

           return Ok(new { success = true, message = "Bilgileriniz başarıyla güncellendi." });
        }


    }
}
