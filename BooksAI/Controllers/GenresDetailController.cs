using Microsoft.AspNetCore.Mvc;
using BooksAI.Models;
using BooksAI.Data;

namespace BooksAI.Controllers
{
    public class GenresDetailController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GenresDetailController(ApplicationDbContext db)
        {
            _db = db;
        }
        private Dictionary<string, string> GetGenreTranslations()
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        {"Fiction", "Художественная литература"},
        {"NonFiction", "Нехудожественная литература"},
        {"Mystery", "Детектив"},
        {"Fantasy", "Фэнтези"},
        {"ScienceFiction", "Научная фантастика"},
        {"Biography", "Биография"},
        {"Romance", "Роман"},
        {"Thriller", "Триллер"},
        {"Horror", "Ужасы"},
        {"HistoricalFiction", "Исторический роман"}
    };
        }
        public IActionResult Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("Error");
            }

            var genreTranslations = GetGenreTranslations();

            if (!genreTranslations.ContainsKey(name))
            {
                return RedirectToAction("Error");
            }

            var books = _db.Books
                .Where(b => b.Genre.ToString().ToLower() == name.ToLower())
                .ToList();

            ViewData["GenreName"] = genreTranslations[name]; 
            return View(books);
        }

    }
}
