
using BooksAI.Data;
using BooksAI.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksAI.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _db;

        public BookService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<OperationResult> AddBookAsync(Book book, IFormFile imageFile)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await imageFile.CopyToAsync(ms);
                    book.ImageData = ms.ToArray();
                    book.ImageMimeType = imageFile.ContentType;
                }

                await _db.Books.AddAsync(book);
                await _db.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Книга добавлена", Book = book };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = ex.Message };
            }
        }
        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _db.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<OperationResult> DeleteBookAsync(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null)
                return new OperationResult { Success = false, Message = "Книга не найдена" };

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();

            return new OperationResult { Success = true, Message = "Книга удалена" };
        }
        public async Task<byte[]> GetBookImageAsync(int id)
        {
            var book = await _db.Books.FindAsync(id);
            return book?.ImageData;
        }
    }
}
