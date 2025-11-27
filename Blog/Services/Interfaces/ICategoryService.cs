using Blog.API.Models.DTOs;

namespace Blog.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();

        Task CreateCategoryAsync(CategoryRequestDTO category);

        Task<CategoryResponseDTO> GetCategoryByIdAsync(int id);

        Task UpdateCategoryByIdAsync(CategoryRequestDTO category, int id);

        Task DeleteCategoryByIdAsync(int id);
    }
}
