using LibraryApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApiProject.Services;
public class CategoryService : ICategoryService
{
    private readonly LibraryContext _context;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(LibraryContext context, ILogger<CategoryService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        try
        {
            return await _context.Categories.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetCategoriesAsync");
            throw;
        }
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        try
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Category created with id {Id}", category.Id);
            return category;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateCategoryAsync");
            throw;
        }
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        try
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;
            bool hasBooks = await _context.Books.AnyAsync(b => b.CategoryId == id);
            if (hasBooks)
                return false;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Category deleted with id {Id}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteCategoryAsync");
            throw;
        }
    }
}

