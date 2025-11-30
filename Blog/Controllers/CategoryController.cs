using Blog.API.Models.DTOs;
using Blog.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        //private ILogger _ilogger;

        // Isso chama-se de 'Construtor dependente', conexão entre CategoryController e CategoryService

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        // Create este Get apenas para testar a conexão
        [HttpGet]
        public ActionResult HertBeat()
        {
            return Ok("Online");
        }


        // Get da lista do meu objeto Category
        [HttpGet("GetAll")] // rota adicional (Api/Category/GetAll
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAllCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            return StatusCode(401, categories);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateCategory(CategoryRequestDTO category)
        {
            await _categoryService.CreateCategoryAsync(category);
            return Created();
        }


        //buscando a categoria pelo Id
        [HttpGet("GetByID/{id}")]
        public async Task<ActionResult<CategoryResponseDTO>> GetCategoryIdAsync(int id)
        {
            //Chama o service para buscar a categoria
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category is null) return NotFound("Categoria não encontrada");

            return Ok(category);
        }



        [HttpPut("UpdateByID/{id}")]
        public async Task<ActionResult<CategoryResponseDTO>> UpdateCategoryAsync(int id, CategoryRequestDTO category)
        {
            var existing = await _categoryService.GetCategoryByIdAsync(id);

            if (existing is null) return NotFound();

            await _categoryService.UpdateCategoryByIdAsync(category, id);

            return Ok(category);
        }

        [HttpDelete("DeleteByID/{id}")]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            var existing = await _categoryService.GetCategoryByIdAsync(id);

            if (existing is null) return NotFound();

            await _categoryService.DeleteCategoryByIdAsync(id);

            return NoContent();
        }

    }
}
