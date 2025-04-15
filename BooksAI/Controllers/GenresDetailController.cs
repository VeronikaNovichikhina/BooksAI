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

        public IActionResult Index(string name)
        {
            // Проверяем, что имя жанра передано
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("Error");
            }

            // Получаем книги жанра (сравниваем как строки)
            var books = _db.Books
                .Where(b => b.Genre.ToString().ToLower() == name.ToLower())
                .ToList();

            ViewData["GenreName"] = name;
            return View(books);
        }

    }
}
