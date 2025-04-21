namespace BooksAI.Models
{
    public class ShoppingCartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
        public decimal TotalPrice { get; set; }
    }

    public class CartItemViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }

        public decimal TotalPrice => Price * Quantity;
    }
}
