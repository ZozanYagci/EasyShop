using Core.Utilities.ApiClients;
using Core.Utilities.Security.JWT;
using DTOs.DTOs.UserDtos;
using EasyShop.UI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EasyShop.UI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index(string tab = "login")
        {
            ViewData["ActiveTab"] = TempData["ActiveTab"] ?? tab;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetAuthCookie([FromBody] AccessToken accessToken)
        {
            if (string.IsNullOrEmpty(accessToken.Token))
            {
                return BadRequest(new { message = "Geçersiz token" });
            }

            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, accessToken.Token)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Çerezin uzun süreli olmasını istersen
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30) // Token süresi 30 dk 
            };

            // Kimlik doğrulama çerezi oluştur
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return Ok(new { message = "Token başarıyla saklandı" });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Default");
        }
    }


}
