using Microsoft.AspNetCore.Mvc;

namespace BooksAI.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
