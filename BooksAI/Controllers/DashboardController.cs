using BooksAI.Data;
using BooksAI.Models;
using BooksAI.Services.BookService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


public class DashboardController : Controller
{
    private readonly IBookService _bookService;
    public DashboardController( IBookService bookService)
    {
        _bookService = bookService;
    }
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
    public async Task<IActionResult> AddBook(Book book, IFormFile imageFile)
    {
        if (!ModelState.IsValid)
        {
            return View("AdminPanel", book);
        }
        
        var result = await _bookService.AddBookAsync(book, imageFile);

        if (result.Success)
        {
            TempData["Success"] = result.Message;
            return RedirectToAction("AdminPanel");
        }

        TempData["Error"] = result.Message;
        return View("AdminPanel", book);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _bookService.DeleteBookAsync(id);

        if (result.Success)
            TempData["SuccessDelete"] = result.Message;
        else
            TempData["Error"] = result.Message;

        return RedirectToAction("AdminPanel");
    }

    public async Task<IActionResult> GetImage(int id)
    {
        var imageData = await _bookService.GetBookImageAsync(id);
        if (imageData == null) return NotFound();

        var book = await _bookService.GetBookByIdAsync(id);
        return File(imageData, book.ImageMimeType);
    }
}
