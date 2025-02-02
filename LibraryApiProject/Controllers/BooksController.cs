using LibraryApiProject.Models;
using LibraryApiProject.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

/// <summary>
/// Controller for managing books.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    /// <summary>
    /// Get books with pagination, filtering, and sorting.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery] string author, [FromQuery] int? publishedYear, [FromQuery] int? categoryId, [FromQuery] int? publisherId, [FromQuery] string sortBy, [FromQuery] bool isDescending = false, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var (books, totalCount) = await _bookService.GetBooksAsync(author, publishedYear, categoryId, publisherId, sortBy, isDescending, page, pageSize);
        var metadata = new
        {
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
        Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(metadata));
        return Ok(books);
    }

    /// <summary>
    /// Get a book by id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
            return NotFound();
        return Ok(book);
    }

    /// <summary>
    /// Create a new book.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] Book book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var createdBook = await _bookService.CreateBookAsync(book);
        return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
    }

    /// <summary>
    /// Update a book.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var updatedBook = await _bookService.UpdateBookAsync(id, book);
        if (updatedBook == null)
            return NotFound();
        return Ok(updatedBook);
    }

    /// <summary>
    /// Delete a book if not loaned.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var result = await _bookService.DeleteBookAsync(id);
        if (!result)
            return BadRequest("Book cannot be deleted or not found.");
        return NoContent();
    }
}

