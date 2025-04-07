using Core.Extensions;
using Core.Utilities.ApiClients;
using DTOs.DTOs.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            int userId = User.GetUserId();
            if (userId <= 0)
                return RedirectToAction("Index", "Default");

            var user = await _apiClient.GetAsync<UserProfileUpdateDto>($"/user/my-profile");

            if (user == null)
                return NotFound();

            return View(user);
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

                return BadRequest(new { errors=valErrors });

            }

            int userId = User.GetUserId();
            if (userId <= 0)
                return RedirectToAction("Index", "Default");

            bool updated = await _apiClient.PutAsync<bool>($"/user/update-profile", model);

            if (!updated)
            {
                return BadRequest(new { success = false, message = "Güncelleme başarısız oldu, lütfen tekrar deneyin." });
            }

            return Ok(new { success = true, message = "Bilgileriniz başarıyla güncellendi." });
        }


    }
}
