namespace BooksAI.Models
{
    public class Book
    {
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Pages { get; set; }
        public decimal Price { get; set; }
    }

}
