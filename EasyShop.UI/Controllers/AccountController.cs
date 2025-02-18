using Azure.Core;
using Core.Utilities.ApiClients;
using DTOs.DTOs.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiClient _apiClient;

        public AccountController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public IActionResult Index(string tab = "login")
        {
            ViewData["ActiveTab"] = tab;
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "";
                foreach (var error in ModelState.Values.SelectMany(x => x.Errors))
                {
                    TempData["Error"] += error.ErrorMessage + "\n";
                }
                return View(userForLoginDto);
            }
            try
            {
                var result = await _apiClient.PostAsync<AccessToken>("Auth/login", userForLoginDto);

                if (string.IsNullOrEmpty(result.Token))
                {
                    TempData["Error"] = "Geçersiz kullanıcı adı veya şifre";
                    return View(userForLoginDto);
                }

                //Token'ı Session'a kaydet
                HttpContext.Session.SetString("JWT", result.Token);
                TempData["Success"] = "Giriş başarılı!";
                return RedirectToAction("Index", "Default");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Giriş sırasında bir hata oluştu.";

                return View(userForLoginDto);
                //TODO: logger 
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "";
                foreach (var error in ModelState.Values.SelectMany(x => x.Errors))
                {
                    TempData["Error"] += error.ErrorMessage + "\n";
                }
                return View(userForRegisterDto);
            }

            try
            {
                var result = await _apiClient.PostAsync<AccessToken>("Auth/register", userForRegisterDto);
                if (string.IsNullOrEmpty(result.Token))
                {
                    TempData["Error"] = "Kayıt işlemi başarısız oldu. Lütfen tekrar deneyiniz.";
                    return View(userForRegisterDto);
                }
                TempData["Success"] = "Kayıt başarılı! Şimdi giriş yapabilirsiniz.";
                return RedirectToAction("Login");

            }
            catch (Exception ex)
            {
                TempData["Error"] = "Bir hata meydana geldi.";

                //TODO: logger

                return View(userForRegisterDto);
            }
        }
    }
}
