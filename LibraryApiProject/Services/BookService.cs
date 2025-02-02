using LibraryApiProject.Services;
using Microsoft.EntityFrameworkCore;

public class BookService : IBookService
{
    private readonly LibraryContext _context;
    private readonly ILogger<BookService> _logger;

    public BookService(LibraryContext context, ILogger<BookService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<(IEnumerable<Book> books, int totalCount)> GetBooksAsync(string author, int? publishedYear, int? categoryId, int? publisherId, string sortBy, bool isDescending, int page, int pageSize)
    {
        try
        {
            var query = _context.Books.Include(b => b.Category).Include(b => b.Publisher).AsQueryable();
            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author.Contains(author));
            }
            if (publishedYear.HasValue)
            {
                query = query.Where(b => b.PublishedYear == publishedYear.Value);
            }
            if (categoryId.HasValue)
            {
                query = query.Where(b => b.CategoryId == categoryId.Value);
            }
            if (publisherId.HasValue)
            {
                query = query.Where(b => b.PublisherId == publisherId.Value);
            }
            var totalCount = await query.CountAsync();
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "title":
                        query = isDescending ? query.OrderByDescending(b => b.Title) : query.OrderBy(b => b.Title);
                        break;
                    case "publishedyear":
                        query = isDescending ? query.OrderByDescending(b => b.PublishedYear) : query.OrderBy(b => b.PublishedYear);
                        break;
                    case "author":
                        query = isDescending ? query.OrderByDescending(b => b.Author) : query.OrderBy(b => b.Author);
                        break;
                    default:
                        query = query.OrderBy(b => b.Id);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(b => b.Id);
            }
            var books = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (books, totalCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetBooksAsync");
            throw;
        }
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        try
        {
            return await _context.Books.Include(b => b.Category)
                                       .Include(b => b.Publisher)
                                       .FirstOrDefaultAsync(b => b.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetBookByIdAsync");
            throw;
        }
    }

    public async Task<Book> CreateBookAsync(Book book)
    {
        try
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Book created with id {Id}", book.Id);
            return book;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateBookAsync");
            throw;
        }
    }

    public async Task<Book> UpdateBookAsync(int id, Book book)
    {
        try
        {
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
                return null;
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.PublishedYear = book.PublishedYear;
            existingBook.ISBN = book.ISBN;
            existingBook.CategoryId = book.CategoryId;
            existingBook.PublisherId = book.PublisherId;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Book updated with id {Id}", id);
            return existingBook;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateBookAsync");
            throw;
        }
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        try
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false;
            bool isLoaned = await _context.Loans.AnyAsync(l => l.BookId == id && l.ReturnDate == null);
            if (isLoaned)
                return false;
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Book deleted with id {Id}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteBookAsync");
            throw;
        }
    }
}

