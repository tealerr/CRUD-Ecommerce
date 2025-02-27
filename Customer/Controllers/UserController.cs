using Microsoft.AspNetCore.Mvc;
using Common.Repositories;
using Common.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Common.Responses;
using Common.Helper;
using Microsoft.EntityFrameworkCore;
namespace Customer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(
            UserManager<IdentityUser> userManager
            )
        {
            _userManager = userManager;
        }

        // POST: register user
        [HttpPost]
        [Route("register")]
        [Authorize(Policy = "ApiPolicy")]
        public async Task<IActionResult> Register([FromBody] RegisterUser user)
        {
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.UserName == user.Email);

            if (existingUser != null)
            {
                return Ok(new BaseResponse().Success("User already exists"));
            }
            UserRepositories repository = new();

            // Add user to Aspnetusers table
            bool IsExternalUser = false;
            var identityUser = new IdentityUser();
            var Newuser = new IdentityUser()
            {
                UserName = user.Email,
                Email = user.Email,
                PhoneNumber = user.Telephone,
                PasswordHash = _userManager.PasswordHasher.HashPassword(identityUser, user.Password)
            };
            var result = await _userManager.CreateAsync(Newuser);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(new BaseResponse().Fail(ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)), "Register Failed"));
            }
            await _userManager.AddToRoleAsync(Newuser, RoleHelper.User);
            if (IsExternalUser)
            {
                var info = new UserLoginInfo(user.SocialProvider, user.SocialId, user.Email);
                await _userManager.AddLoginAsync(Newuser, info);
            }

            // Get user data after crerated
            var createdUser = await _userManager.FindByEmailAsync(Newuser.Email);
            if (createdUser == null)
            {
                return BadRequest(new { Message = "User not registered" });
            }

            // Add user data to table User
            var addUserresult = await repository.AddUserAsync(user, createdUser.Id);
            if (!addUserresult)
            {
                return BadRequest(new { Message = "User not registered" });
            }

            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPut]
        [Route("update-infos")]
        [Authorize(Policy = "LoginPolicy")]
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
    }
}