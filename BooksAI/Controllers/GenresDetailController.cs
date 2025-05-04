using Microsoft.AspNetCore.Mvc;
using BooksAI.Models;
using BooksAI.Data;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet]
        public IActionResult Index(string name, int page = 1, int pageSize = 6)
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
            ViewBag.GenreKey = name;
            ViewBag.Filter = new BookFilterModel();

            return View(books);
        }
        [HttpPost]
        public IActionResult Index(string name, BookFilterModel filter)
        {
            var books = _db.Books.Where(b => b.Genre.ToString().ToLower() == name.ToLower());

            if (filter.PriceMin.HasValue)
                books = books.Where(b => b.Price >= filter.PriceMin.Value);
            if (filter.PriceMax.HasValue)
                books = books.Where(b => b.Price <= filter.PriceMax.Value);
            if (filter.YearMin.HasValue)
                books = books.Where(b => b.Year >= filter.YearMin.Value);
            if (filter.YearMax.HasValue)
                books = books.Where(b => b.Year <= filter.YearMax.Value);
            if (!string.IsNullOrWhiteSpace(filter.Search))
                books = books.Where(b => b.Title.Contains(filter.Search) || b.Author.Contains(filter.Search));

            var genreTranslations = GetGenreTranslations();
            ViewData["GenreName"] = genreTranslations[name];
            ViewBag.GenreKey = name;
            ViewBag.Filter = filter;

            return View(books.ToList());
        }


    }
}
