using LibraryApiProject.Models;

using LibraryApiProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// <summary>
/// Controller for managing categories.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Get all categories.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        return Ok(categories);
    }

    /// <summary>
    /// Create a new category.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] Category category)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var createdCategory = await _categoryService.CreateCategoryAsync(category);
        return CreatedAtAction(nameof(GetCategories), new { id = createdCategory.Id }, createdCategory);
    }

    /// <summary>
    /// Delete a category if no book is associated.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _categoryService.DeleteCategoryAsync(id);
        if (!result)
            return BadRequest("Category cannot be deleted or not found.");
        return NoContent();
    }
}