namespace LibraryApiProject.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApiProject.Models;

public interface IBookService
{
    Task<(IEnumerable<Book> books, int totalCount)> GetBooksAsync(string author, int? publishedYear, int? categoryId, int? publisherId, string sortBy, bool isDescending, int page, int pageSize);
    Task<Book> GetBookByIdAsync(int id);
    Task<Book> CreateBookAsync(Book book);
    Task<Book> UpdateBookAsync(int id, Book book);
    Task<bool> DeleteBookAsync(int id);
}

