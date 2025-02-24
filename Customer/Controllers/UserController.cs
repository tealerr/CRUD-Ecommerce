using Microsoft.AspNetCore.Mvc;
using Common.Repositories;
using Common.Request;
namespace Customer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        // POST: register user
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser user)
        {
            UserRepositories repository = new();

            // Add user
            var result = await repository.AddUserAsync(user);
            if (!result)
            {
                return BadRequest(new { Message = "User not registered" });
            }

            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPut]
        [Route("update-infos")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUser user)
        {
            try
            {
                UserRepositories repository = new();

                // Update user data
                var result = await repository.UpdateUserAsync(user);

                if (!result)
                {
                    return NotFound(new { Message = "User not found" });
                }

                return Ok(new { Message = "User updated successfully" });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { Message = "Invalid data provided", Details = ex.Message });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating user: {ex.Message}");

                return StatusCode(500, new { Message = "An unexpected error occurred", Details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Login()
        {
            // This is just a placeholder. Replace with actual logic to update a user.
            return NoContent();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            return NoContent();
        }

    }
}