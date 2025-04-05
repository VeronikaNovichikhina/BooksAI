using Microsoft.AspNetCore.Mvc;

namespace BooksAI.Controllers
{
    public class GenresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
