using LibraryApiProject.Models;

using LibraryApiProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// <summary>
/// Controller for managing users.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get all users (Administrateurs uniquement).
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }

    /// <summary>
    /// Create a new user.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var createdUser = await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUsers), new { id = createdUser.Id }, createdUser);
    }

    /// <summary>
    /// Delete a user.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userService.DeleteUserAsync(id);
        if (!result)
            return NotFound();
        return NoContent();
    }
}

