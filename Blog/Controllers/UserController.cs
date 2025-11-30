using Blog.API.Models.DTOs;
using Blog.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userServiice)
        {
            _userService = userServiice;
        }



        [HttpGet]
        public ActionResult HeartBeat()
        {
            return Ok("Online");
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<List<UserResponseDTO>>> GetAllUsersAsync()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }


        [HttpPost("Create")]
        public async Task<ActionResult> CreateUserAsync(UserRequestDTO user)
        {
            await _userService.CreateUserAsync(user);

            return Created();
        }


        [HttpGet("GetByID/{id}")]
        public async Task<ActionResult<UserResponseDTO>> GetUsersByIdAsync(int id)
        {
            return Ok(await _userService.GetUserByIdAsync(id));
        }


        [HttpPut("UpdateByID/{id}")]
        public async Task<ActionResult> UpdateRoleById(UserRequestDTO user, int id)
        {
            var userFound = await _userService.GetUserByIdAsync(id);

            if (userFound is null)
            {
                return NotFound();
            }

            await _userService.UpdateUserByIdAsync(user, id);
            return Ok();
        }


        [HttpDelete("DeleteByID/{id}")]
        public async Task<ActionResult> DeleteUserByIDAsync(int id)
        {
            var userFound = await _userService.GetUserByIdAsync(id);

            if (userFound is null)
            {
                return NotFound();
            }

            await _userService.DeleteUserByIdAsync(id);
            return NoContent();
        }




        [HttpGet("GetUserRoles")]
        public async Task<ActionResult<List<UserRolesResponseDTO>>> GetUserRolesAsync()
        {
            var users = await _userService.GetAllUserRolesAsync();
            return Ok(users);
        }



        [HttpGet("GetUserRolesByID/{id}")]
        public async Task<ActionResult<List<UserResponseDTO>>> GetUserRolesByIDAsync(int id)
        {
            var users = await _userService.GetUserRolesByIdAsync(id);
            return Ok(users);
        }





    }
}
