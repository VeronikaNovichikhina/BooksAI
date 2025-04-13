using BooksAI.Data;
using BooksAI.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Настройка сервисов
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var connectionString = builder.Configuration.GetConnectionString("MSSQL");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddSession();

var app = builder.Build();

app.UseSession();
app.UseRouting();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


    if (!context.Users.Any(u => u.Email == "admin@example.com"))
    {
        var passwordHasher = new PasswordHasher<User>();  
        var hashedPassword = passwordHasher.HashPassword(null, "Admin123");  // Хешируем пароль

        context.Users.Add(new User
        {
            Email = "admin@example.com",
            Password = hashedPassword, 
            Role = "Admin"
        });
        context.SaveChanges();
    }
}

app.Run();
