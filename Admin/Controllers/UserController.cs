using Microsoft.AspNetCore.Mvc;
using Common.Repositories;
using Common.Models;

namespace Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        [HttpPost]
        [Route("users")]
        public IActionResult GetUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            UserRepositories repository = new();

            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var users = repository.GetUserList(pageNumber, pageSize);
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }

            return Ok(new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalUsers = users.Count,
                Users = users
            });
        }

        [HttpPut]
        [Route("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            UserRepositories repository = new UserRepositories();

            // update user data
            var result = await repository.UpdateUserAsync(user);

            if (!result)
            {
                return BadRequest(new { Message = "User not found" });
            }

            return Ok(new { Message = "User updated successfully" });
        }
    }
}