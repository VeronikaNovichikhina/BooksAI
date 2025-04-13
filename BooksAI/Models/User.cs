using Microsoft.AspNetCore.Identity;

namespace BooksAI.Models
{
    public class User 
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Role { get; set; } = "User";

        public bool IsAdmin => Email == "admin@mail.com";//вставите почту админа
    }

}
