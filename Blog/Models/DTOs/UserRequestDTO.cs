namespace Blog.API.Models.DTOs
{
    public class UserRequestDTO
    {
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string PasswordHash { get; init; } = string.Empty;
        public string Bio { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty;
    }
}
