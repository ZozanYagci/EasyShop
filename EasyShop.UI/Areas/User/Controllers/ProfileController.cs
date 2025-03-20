using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.Areas.User.Controllers
{
    [Area("User")]
    public class ProfileController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
