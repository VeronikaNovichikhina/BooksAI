using BooksAI.Data;
using BooksAI.Models;
using Microsoft.AspNetCore.Mvc;

public class DashboardController : Controller
{
    private readonly ApplicationDbContext _db;

    public DashboardController(ApplicationDbContext db)
    {
        _db = db;
    }

    // GET: /Dashboard/AdminPanel
    public IActionResult AdminPanel()
    {
        if (HttpContext.Session.GetString("UserEmail") != "admin@example.com")
        {
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Message = "Добро пожаловать, Администратор!";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(Book book)
    {

        if (!ModelState.IsValid)
        {
            // Вывод ошибок в консоль
            foreach (var entry in ModelState)
            {
                if (entry.Value.Errors.Count > 0)
                {
                    Console.WriteLine($"{entry.Key}: {string.Join(", ", entry.Value.Errors.Select(e => e.ErrorMessage))}");
                }
            }
            return View("AdminPanel", book);
        }


        try
        {

            _db.Books.Add(book);
            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Книга успешно добавлена!";
            return RedirectToAction("AdminPanel");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Ошибка: " + ex.Message;
            return View("AdminPanel", book);
        }
    }
}
