namespace BooksAI.Models
{
    public class BasketItem
    {
        public Book Book { get; set; }
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserEmail { get; set; }
    }

}
