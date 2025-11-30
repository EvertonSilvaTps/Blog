using Blog.API.Data;
using Blog.API.Models;
using Blog.API.Models.DTOs;
using Blog.API.Repositories.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace Blog.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _connection;

        public UserRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }


        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            var sql = "SELECT Name, Email, PasswordHash, Bio, Image, Slug FROM User";
            return (await _connection.QueryAsync<UserResponseDTO>(sql)).ToList();
        }


        public async Task CreateUserAsync(User user)
        {
            var sql = "INSERT INTO [User] (Name, Email, PasswordHash, Bio, Image, Slug) VALUES (@Name, @Email, @PasswordHash, @Bio, @Image, @Slug)";
            await _connection.ExecuteAsync(sql, new { user.Name, user.Email, user.PasswordHash, user.Bio, user.Image, user.Slug });
        }



        public async Task<UserResponseDTO> GetUserByIdAsync(int id)
        {
            var sql = "SELECT Name, Email, PasswordHash, Bio, Image, Slug FROM User WHERE Id = @Id";
            return (await _connection.QueryFirstOrDefaultAsync<UserResponseDTO>(sql, new { Id = id }));
        }



        public async Task UpdateUserByIdAsync(User user, int id)
        {
            var sql = "UPDATE User SET Name = @Name, Email = @Email, PasswordHash = @PasswordHash, Bio = @Bio, Image = @Image, Slug = @Slug WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { user.Name, user.Email, user.PasswordHash, user.Bio, user.Image, user.Slug, CategoryID = id });
        }


        public async Task DeleteUserByIdAsync(int id)
        {
            var sql = "DELETE FROM User WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }




        public async Task<List<User>> GetAllUserRolesAsync()
        {
            // IEnumerable<User> userRoles = new List<User>();  // Convert para uma lista type User
            
            var sql = @"SELECT u.Id, u.Name, u.Email, u.PasswordHash, u.Bio, u.Image, u.Slug, 
                        r.Id AS Id, r.Name, r.Slug
                        FROM [User] u
                        LEFT JOIN [UserRole] ur
                        ON u.Id = ur.UserId
                        LEFT JOIN [Role] r
                        ON r.Id = ur.RoleId";

            var userDictionary = new Dictionary<int, User>();

            var results = await _connection.QueryAsync<User, Role, User>(
                    sql, (user, role) => {

                        if (!userDictionary.TryGetValue(user.Id, out var existingUser))
                        {
                            userDictionary.Add(user.Id, user);
                            existingUser = user;
                        }

                        if (role != null)
                        {
                            existingUser.Roles.Add(role);
                        }
                        return existingUser;
                    },
                    splitOn: "Id"
                );

            return userDictionary.Values.ToList();
        }


        public async Task<User> GetUserRolesByIdAsync(int id)
        {

            var sql = @"SELECT u.Id, u.Name, u.Email, u.PasswordHash, u.Bio, u.Image, u.Slug, 
                        r.Id AS Id, r.Name, r.Slug
                        FROM [User] u
                        LEFT JOIN [UserRole] ur
                        ON u.Id = ur.UserId
                        LEFT JOIN [Role] r
                        ON r.Id = ur.RoleId 
                        WHERE u.Id = @UserId";

            var userDictionary = new Dictionary<int, User>();

            await _connection.QueryAsync<User, Role, User>(
                sql, (user, role) =>
                {
                    if (!userDictionary.TryGetValue(user.Id, out var existingUser))
                    {
                        userDictionary.Add(user.Id, user);
                        existingUser = user;
                    }

                    if (role != null)
                    {
                        existingUser.Roles.Add(role);
                    }

                    return existingUser;
                },

                param: new { UserId = id },
                splitOn: "Id"
            );
            return userDictionary.Values.FirstOrDefault();
        }


    }
}
