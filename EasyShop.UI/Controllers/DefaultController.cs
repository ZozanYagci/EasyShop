﻿using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "Default";
            return View();
        }
    }
}
