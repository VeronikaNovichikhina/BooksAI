using Microsoft.AspNetCore.Mvc;
using BooksAI.Data;
using BooksAI.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

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
            if (email == "lipchanskayakatya@gmail.com")
            {
                ViewBag.Error = "Этот email принадлежит администратору.";
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                ViewBag.Error = "Неправильный email или пароль.";
                return View();
            }

            HttpContext.Session.SetString("UserEmail", email);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string email, string password)
        {
            if (email == "lipchanskayakatya@gmail.com")
            {
                ViewBag.Error = "Этот email принадлежит администратору.";
                return View();
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
            if (existingUser != null)
            {
                ViewBag.Error = "Пользователь с таким email уже существует.";
                return View();
            }

            var user = new User { Email = email, Password = password };
            _context.Users.Add(user);
            _context.SaveChanges();

            HttpContext.Session.SetString("UserEmail", email);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserEmail");
            return RedirectToAction("Login");
        }
    }
}
