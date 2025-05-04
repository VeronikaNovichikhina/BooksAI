using Microsoft.AspNetCore.Mvc;
using BooksAI.Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using BooksAI.Models.ViewModel;

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
        public IActionResult Index(string name, int page = 1, int pageSize = 3)
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

            var query = _db.Books
                .Where(b => b.Genre.ToString().ToLower() == name.ToLower());
                
            int totalBooks = query.Count();
            var books = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            PaginatedBooksViewModel model = new PaginatedBooksViewModel(totalBooks, page, pageSize);
            IndexViewModel indexModel = new IndexViewModel(books, new BookFilterModel(), model);


            ViewData["GenreName"] = genreTranslations[name];
            ViewBag.GenreKey = name;
            

            return View(indexModel);
        }
        [HttpPost]
        public IActionResult Index(string name, BookFilterModel filter, int page = 1, int pageSize = 3)
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

            int totalBooks = books.Count();
            var bk= books
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            PaginatedBooksViewModel model = new PaginatedBooksViewModel(totalBooks, page, pageSize);
            IndexViewModel indexModel = new IndexViewModel(bk, filter, model);

            ViewData["GenreName"] = genreTranslations[name];
            ViewBag.GenreKey = name;
            

            return View(indexModel);
        }


    }
}
