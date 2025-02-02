using LibraryApiProject.Services;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly LibraryContext _context;
    private readonly ILogger<UserService> _logger;

    public UserService(LibraryContext context, ILogger<UserService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        try
        {
            return await _context.Users.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetUsersAsync");
            throw;
        }
    }

    public async Task<User> CreateUserAsync(User user)
    {
        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User created with id {Id}", user.Id);
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateUserAsync");
            throw;
        }
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User deleted with id {Id}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteUserAsync");
            throw;
        }
    }
}
