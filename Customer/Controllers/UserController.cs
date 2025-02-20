using Microsoft.AspNetCore.Mvc;
using Common.Models;
using Common.Repositories;
namespace Customer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        // POST: register user
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            UserRepositories repository = new UserRepositories();

            // check duplicate user
            string existingUser = "";
            if (user.UserGuid == existingUser)
            {
                return BadRequest(new { Message = "User already exists" });
            }

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