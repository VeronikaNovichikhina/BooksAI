using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAI.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public string BasketId { get; set; }
    }


}
