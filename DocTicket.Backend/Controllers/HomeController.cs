﻿using Microsoft.AspNetCore.Mvc;

namespace DocTicket.Backend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
