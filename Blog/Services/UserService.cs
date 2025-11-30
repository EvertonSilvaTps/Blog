using Blog.API.Models;
using Blog.API.Models.DTOs;
using Blog.API.Repositories.Interfaces;
using Blog.API.Services.Interfaces;

namespace Blog.API.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<UserResponseDTO> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }


        public async Task CreateUserAsync(UserRequestDTO user)
        {
            var newUser = new User(user.Name, user.Email, user.PasswordHash, user.Bio, user.Image, user.Name.ToLower().Replace(" ", "-"));
            await _userRepository.CreateUserAsync(newUser);
        }


        public async Task UpdateUserByIdAsync(UserRequestDTO user, int id)
        {
            var newUser = new User(user.Name, user.Email, user.PasswordHash, user.Bio, user.Image, user.Name.ToLower().Replace(" ", "-"));
            await _userRepository.UpdateUserByIdAsync(newUser, id);
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            await _userRepository.DeleteUserByIdAsync(id);
        }



        public async Task<List<UserRolesResponseDTO>> GetAllUserRolesAsync()
        {
            var users = await _userRepository.GetAllUserRolesAsync();

            var result = users.Select(u => new UserRolesResponseDTO
            {
                Name = u.Name,
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                Bio = u.Bio,
                Image = u.Image,
                Slug = u.Slug,
                    
                Roles = u.Roles.Select(r => new RoleResponseDTO
                    {
                        Name = r.Name,
                        Slug = r.Slug
                    }).ToList()
            }).ToList();
            
            return result;
        }



        public async Task<UserRolesResponseDTO> GetUserRolesByIdAsync(int id)
        {
            var user = await _userRepository.GetUserRolesByIdAsync(id);

            if (user is null) return null;

            var result = new UserRolesResponseDTO
            {
                Name = user.Name,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Bio = user.Bio,
                Image = user.Image,
                Slug = user.Slug,

                Roles = user.Roles?.Select(role => new RoleResponseDTO
                {
                    Name = role.Name,
                    Slug = role.Slug
                }).ToList()!
            };
            
            return result;
        }


    }
}
