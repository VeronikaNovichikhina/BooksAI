
using BooksAI.Data;
using BooksAI.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksAI.Services.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly ApplicationDbContext _context;

        public BasketService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async  Task AddToCartAsync(string basketId, int bookId, int quantity)
        {
            var items = await _context.BasketItems
                .FirstOrDefaultAsync(b => b.BookId == bookId && b.BasketId == basketId);

            if(items == null)
            {
                items = new BasketItem
                {
                    BookId = bookId,
                    Quantity = quantity,
                    BasketId = basketId
                };
                _context.BasketItems.Add(items);
            }
            else
            {
                items.Quantity += quantity;
            }
            await _context.SaveChangesAsync();
        }

        public async  Task<List<BasketItem>> GetItemsAsync(string basketId)
        {
           return await _context.BasketItems
                .Include(b => b.Book)
                .Where(b => b.BasketId == basketId)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalAsync(string basketId)
        {
            var items = await GetItemsAsync(basketId);
            return items.Sum(b => b.Book.Price * b.Quantity);
        }

        public async Task RemoveItemAsync(int itemId)
        {
            var items = await _context.BasketItems.FindAsync(itemId);
            if (items != null)
            {
                _context.BasketItems.Remove(items);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateQuantityAsync(int itemId, int quantity)
        {
            var items = await _context.BasketItems.FindAsync(itemId);
            if (items != null)
            {
                if(quantity <= 0)
                {
                    _context.BasketItems.Remove(items);
                }
                else
                {
                    items.Quantity = quantity;
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
