﻿using Microsoft.AspNetCore.Mvc;

namespace BooksAI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
