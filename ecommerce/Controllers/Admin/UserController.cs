using Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        [HttpPost]
        [Route("users")]
        public IActionResult GetUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            UserRepositories repository = new UserRepositories();

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

        // PUT: api/user/edit
        [HttpPut("edit")]
        public IActionResult EditUser()
        {
            // This is just a placeholder. Replace with actual logic to edit a user.
            return Ok();
        }
    }
}