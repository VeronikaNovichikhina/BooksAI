using BooksAI.Data;
using Microsoft.AspNetCore.Mvc;

namespace BooksAI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Index", "Home");
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
