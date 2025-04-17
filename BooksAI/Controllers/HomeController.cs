using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksAI.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            
            return View();
        }
        
    }
}
