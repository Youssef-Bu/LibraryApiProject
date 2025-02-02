namespace LibraryApiProject.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApiProject.Models;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> CreateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);
}

