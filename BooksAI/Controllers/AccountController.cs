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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (email == "admin@example.com")
            {
                ViewBag.Error = "Этот email принадлежит администратору.";
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null || !VerifyPassword(user.Password, password))  // Проверка пароля
            {
                ViewBag.Error = "Неправильный email или пароль.";
                return View();
            }

            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role);
            return RedirectToAction("Index", "Home");
        }

        // Метод для проверки пароля
        private bool VerifyPassword(string storedHash, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(null, storedHash, password);
            return result == PasswordVerificationResult.Success;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserEmail");
            return RedirectToAction("Login");
        }
    }
}
