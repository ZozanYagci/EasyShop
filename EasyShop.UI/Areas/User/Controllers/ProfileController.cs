using Core.Extensions;
using Core.Utilities.ApiClients;
using Core.Utilities.Results;
using DTOs.DTOs.UserDtos;
using EasyShop.UI.Areas.User.Models;
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

        public async Task<IActionResult> Index()
        {
            var userProfile = await _apiClient.GetAsync<UserProfileUpdateDto>("User/my-profile");

            var model = new UserProfileViewModel
            {
                ProfileUpdate = userProfile,
                ChangePassword = new ChangePasswordDto()

            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ProfileUpdate(UserProfileUpdateDto model)
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

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
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

            try
            {
                var response = await _apiClient.PutAsync<Result>("User/change-password", model);

                if (response is null)
                {
                    return BadRequest(new { success = false, message = "Sunucudan yanıt alınamadı." });
                }

                if (!response.Success)
                {
                    return BadRequest(new { success = false, message = response.Message });
                }

                return Ok(new { success = true, message = response.Message });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Sunucuda bir hata oluştu. Detay: " + ex.Message
                });
            }

        }
    }
}