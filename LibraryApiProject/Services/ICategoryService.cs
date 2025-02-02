namespace LibraryApiProject.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApiProject.Models;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category> CreateCategoryAsync(Category category);
    Task<bool> DeleteCategoryAsync(int id);
}

