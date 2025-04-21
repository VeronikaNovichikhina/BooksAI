using BooksAI.Data;
using BooksAI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BooksAI.Controllers
{
    
    public class BasketController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BasketController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            
            var userId = User.Identity.IsAuthenticated
                    ? User.FindFirstValue(ClaimTypes.NameIdentifier)
                    : HttpContext.Session.GetString("GuestId");

            if (string.IsNullOrEmpty(userId))
            {
                return View(new List<CartItem>()); // пустая корзина
            }

            Console.WriteLine("Index userId: " + userId);

            var cart = await _db.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            var items = cart?.Items ?? new List<CartItem>();

            return View(items);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookId)
        {
            
            string userId = User.Identity.IsAuthenticated
                ? User.FindFirstValue(ClaimTypes.NameIdentifier)
                    : HttpContext.Session.GetString("GuestId");

            if (string.IsNullOrEmpty(userId))
            {
                userId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("GuestId", userId);
            }

            var cart = await _db.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId, CreatedDate = DateTime.Now };
                _db.Carts.Add(cart);
                await _db.SaveChangesAsync();
            }

            var cartItem = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                var book = await _db.Books.FindAsync(bookId);
                if (book == null)
                {
                    return NotFound(new { success = false, message = "Книга не найдена." });
                }

                cart.Items.Add(new CartItem
                {
                    BookId = bookId,
                    Quantity = 1,
                    Year = book.Year
                });
            }

            await _db.SaveChangesAsync();

            cart = await _db.Carts.Include(c => c.Items).ThenInclude(i => i.Book).FirstOrDefaultAsync(c => c.UserId == userId);

            return Json(new { success = true, message = "Книга добавлена в корзину" });

        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(int bookId, int quantity)
        {
            string userId = User.Identity.IsAuthenticated
                ? User.FindFirstValue(ClaimTypes.NameIdentifier)
                : HttpContext.Session.GetString("GuestId");

            var cart = await _db.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null) return Json(new { success = false });

            var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (item == null) return Json(new { success = false });

            item.Quantity = quantity;
            await _db.SaveChangesAsync();

            decimal updatedPrice = item.Book.Price * item.Quantity;
            decimal totalPrice = cart.Items.Sum(i => i.Book.Price * i.Quantity);

            return Json(new
            {
                success = true,
                price = updatedPrice.ToString("C"),  // <-- для JS
                total = totalPrice.ToString("C")     // <-- для JS
            });
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int bookId)
        {
            
            string userId = User.Identity.IsAuthenticated
                ? User.FindFirstValue(ClaimTypes.NameIdentifier)
                : HttpContext.Session.GetString("GuestId");

            var cart = await _db.Carts.Include(c => c.Items).ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            var item = cart?.Items.FirstOrDefault(i => i.BookId == bookId);
            if (item != null)
            {
                cart.Items.Remove(item);
                await _db.SaveChangesAsync();
            }

            decimal total = cart?.Items.Sum(i => i.Book.Price * i.Quantity) ?? 0;

            return Json(new { success = true, total = total.ToString("C") });
        }
    }
}
