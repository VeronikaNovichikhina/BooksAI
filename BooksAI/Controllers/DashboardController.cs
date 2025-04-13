using Microsoft.AspNetCore.Mvc;

public class DashboardController : Controller
{
    public IActionResult AdminPanel()
    {
        if (HttpContext.Session.GetString("UserEmail") != "admin@example.com")
        {
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Message = "Добро пожаловать, Администратор!";
        return View();
    }
}
