using BooksAI.Models;

namespace BooksAI.Services.BasketService
{
    public interface IBasketService
    {
        Task<List<BasketItem>> GetItemsAsync(string basketId);
        Task<decimal> GetTotalAsync(string basketId);
        Task AddToCartAsync(string basketId, int bookId, int quantity);
        Task UpdateQuantityAsync(int itemId, int quantity);
        Task RemoveItemAsync(int itemId);


    }
}
