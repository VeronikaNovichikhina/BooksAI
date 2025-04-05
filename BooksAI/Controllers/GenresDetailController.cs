using Microsoft.AspNetCore.Mvc;

namespace BooksAI.Controllers
{
    public class GenresDetailController : Controller
    {
        public IActionResult Index(string name)
        {
            ViewData["GenreName"] = name;
           

            var books = new List<Book>
        {
            new Book { Title = "Книга 1", Author = "Автор 1", Pages = "qwerrghjklkjhgfdsfghjkqwertyuiop[sdfghjkl;sdfghjk", Price = 500 },
            new Book { Title = "Книга 2", Author = "Автор 2", Pages = "qwerrghjklkjhgfdsfghjk", Price = 600 },
            new Book { Title = "Книга 3", Author = "Автор 3", Pages = "qwerrghjklkjhgfdsfghjk", Price = 300 },
            new Book { Title = "Книга 4", Author = "Автор 4", Pages = "qwerrghjklkjhgfdsfghjk", Price = 700 },
            new Book { Title = "Книга 1", Author = "Автор 1", Pages = "qwerrghjklkjhgfdsfghjk", Price = 500 },
            new Book { Title = "Книга 6", Author = "Автор 6", Pages = "qwerrghjklkjhgfdsfghjk", Price = 600 },
            new Book { Title = "Книга 2", Author = "Автор 2", Pages = "qwerrghjklkjhgfdsfghjk", Price = 300 },
            new Book { Title = "Книга 8", Author = "Автор 8", Pages = "qwerrghjklkjhgfdsfghjk", Price = 700 },
            new Book { Title = "Книга 1", Author = "Автор 1", Pages = "qwerrghjklkjhgfdsfghjk", Price = 500 },
            new Book { Title = "Книга 3", Author = "Автор 3", Pages = "qwerrghjklkjhgfdsfghjk", Price = 600 },
            new Book { Title = "Книга 3", Author = "Автор 3", Pages = "qwerrghjklkjhgfdsfghjk", Price = 300 },
            new Book { Title = "Книга 12", Author = "Автор 12", Pages = "qwerrghjklkjhgfdsfghjk", Price = 700 }
        };

            return View(books);
        }
        public class Book
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Pages { get; set; }
            public decimal Price { get; set; }
        }
    }
}
