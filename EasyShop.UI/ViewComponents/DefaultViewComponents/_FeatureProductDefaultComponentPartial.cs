﻿using Microsoft.AspNetCore.Mvc;

namespace EasyShop.UI.ViewComponents.DefaultViewComponents
{
    public class _FeatureProductDefaultComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();  
        }
    }
}
