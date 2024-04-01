﻿using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect(url:"~/swagger");
        }
    }
}
