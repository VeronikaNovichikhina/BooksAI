namespace BooksAI.Models.ViewModel
{
    public class PaginatedBooksViewModel
    {
        public PaginatedBooksViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public int PageNumber { get;  }

        public int TotalPages { get;  }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        
    }

    public class IndexViewModel
    {
        public IEnumerable<Book> Books { get; }
        public BookFilterModel Filter { get;  }
        public PaginatedBooksViewModel Pagination { get; }

        public IndexViewModel(IEnumerable<Book> books, BookFilterModel filter, PaginatedBooksViewModel pagination)
        {
            Books = books;
            Filter = filter;
            Pagination = pagination;
        }
    }
}
