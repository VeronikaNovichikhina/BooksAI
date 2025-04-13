using Microsoft.AspNetCore.Mvc;
using BooksAI.Data;
using BooksAI.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BooksAI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult Login(string email, string password)
        {

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null || !VerifyPassword(user.Password, password))  // Проверка пароля
            {
                TempData["Error"] = "Неправильный email или пароль.";
                return RedirectToAction("Index", "Home");
            }

            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role);

            if (user.Role == "Admin")
            {
                return RedirectToAction("AdminPanel", "Dashboard"); 
            }

            return RedirectToAction("Index", "Home");
        }

        // Метод для проверки пароля
        private bool VerifyPassword(string storedHash, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(null, storedHash, password);
            return result == PasswordVerificationResult.Success;
        }


        [HttpPost]
        public IActionResult Register(string email, string password)
        {
            if (email == "admin@example.com")
            {
                TempData["Error"] = "Этот email принадлежит администратору.";
                return RedirectToAction("Index", "Home");
            }
            if (_context.Users.Any(u => u.Email == email))
            {
                TempData["Error"] = "Пользователь с таким email уже существует.";
                return RedirectToAction("Index", "Home");
            }

            var passwordHasher = new PasswordHasher<User>();
            var user = new User
            {
                Email = email,
                Password = passwordHasher.HashPassword(null, password), // Хеширование пароля
                Role = "User"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserEmail");
            return RedirectToAction("Login");
        }
    }
}
