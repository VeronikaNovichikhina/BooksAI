using BooksAI.Models;

namespace BooksAI.Services.BookService
{
    public interface IBookService
    {
        Task<OperationResult> AddBookAsync(Book book, IFormFile imageFile);
        Task<OperationResult> DeleteBookAsync(int id);
        Task<byte[]> GetBookImageAsync(int id);
        Task<Book> GetBookByIdAsync(int id);
    }
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Book Book { get; set; }
    }
}
