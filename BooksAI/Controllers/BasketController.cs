using BooksAI.Data;
using BooksAI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksAI.Controllers
{
    public class BasketController : Controller
    {
        
        public async Task<IActionResult> Index()
        {
            
            return View();
        }
    }
}
