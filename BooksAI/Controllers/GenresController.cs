using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
