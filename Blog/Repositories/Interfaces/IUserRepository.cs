using Blog.API.Models;
using Blog.API.Models.DTOs;

namespace Blog.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserResponseDTO>> GetAllUsersAsync();

        Task CreateUserAsync(User user);

        Task<UserResponseDTO> GetUserByIdAsync(int id);

        Task UpdateUserByIdAsync(User user, int id);

        Task DeleteUserByIdAsync(int id);

        Task<List<User>> GetAllUserRolesAsync();

        Task<User> GetUserRolesByIdAsync(int id);
    }
}
