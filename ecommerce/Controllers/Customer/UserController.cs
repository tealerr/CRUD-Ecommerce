using Microsoft.AspNetCore.Mvc;
using ecommerce.Models;
using System.Threading.Tasks;
using Common.Repositories;

namespace Ecommerce.Controllers.Customer
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
            await repository.AddUserAsync(user);

            return Ok(new { Message = "User registered successfully" });
        }

        // PUT: api/user/{id}
        [HttpPut]
        [Route("update-infos")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            UserRepositories repository = new UserRepositories();
            // check duplicate user
            string existingUser = "";
            if (user.UserGuid == existingUser)
            {
                return BadRequest(new { Message = "User already exists" });
            }

            // Add user
            await repository.UpdateUserAsync(user);

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