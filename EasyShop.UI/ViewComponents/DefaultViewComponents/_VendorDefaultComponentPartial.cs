﻿using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.ViewComponents.DefaultViewComponents
{
    public class _VendorDefaultComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}