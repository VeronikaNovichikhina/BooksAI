using BooksAI.Data;
using BooksAI.Models;
using BooksAI.Services.BookService;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim("IsAdmin", "true"));
});

builder.Services.AddSession();

var connectionString = builder.Configuration.GetConnectionString("MSSQL");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});


var app = builder.Build();

app.UseSession();
app.UseRouting();
app.MapStaticAssets();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


    if (!context.Users.Any(u => u.Email == "admin@example.com"))
    {
        var passwordHasher = new PasswordHasher<User>();
        var hashedPassword = passwordHasher.HashPassword(null, "Admin123");
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
