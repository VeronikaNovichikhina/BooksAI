namespace BooksAI.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }

    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int Quantity { get; set; } = 1;
        public string Year { get; set; }
    }

}
