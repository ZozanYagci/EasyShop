﻿using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.ViewComponents.UILayoutViewComponents
{
    public class _HeaderComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
