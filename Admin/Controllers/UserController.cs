using Microsoft.AspNetCore.Mvc;
using Common.Repositories;
using Common.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Common.Responses;
using Common.Helper;

namespace Admin.Controllers
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

        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> GetUsers(
        [FromHeader(Name = "Authorization")] string bearerToken,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
            if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer "))
            {
                return Unauthorized("Invalid or missing token.");
            }

            var userGuid = TokenHelper.GetUserGuidFromToken(bearerToken);
            if (userGuid == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            if (_userManager == null)
            {
                return StatusCode(500, "User manager not found.");
            }

            RoleHelper roleHelper = new(_userManager);
            bool isAdmin = await roleHelper.IsAdmin(userGuid);

            if (!isAdmin)
            {
                return Unauthorized("User is not authorized to access this resource.");
            }

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
        public async Task<IActionResult> UpdateUser([FromHeader(Name = "Authorization")] string bearerToken, [FromBody] UpdateUser user)
        {
            if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer "))
            {
                return Unauthorized("Invalid or missing token.");
            }

            var userGuid = TokenHelper.GetUserGuidFromToken(bearerToken);
            if (userGuid == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            if (_userManager == null)
            {
                return StatusCode(500, "User manager not found.");
            }

            RoleHelper roleHelper = new(_userManager);
            bool isAdmin = await roleHelper.IsAdmin(userGuid);

            if (!isAdmin)
            {
                return Unauthorized("User is not authorized to access this resource.");
            }
            UserRepositories repository = new();

            // update user data
            var result = await repository.UpdateUserAsync(user);

            if (!result)
            {
                return BadRequest(new { Message = "User not found" });
            }

            return Ok(new { Message = "User updated successfully" });
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
                return BadRequest(new BaseResponse().Fail("User already exists"));
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
            await _userManager.AddToRoleAsync(Newuser, RoleHelper.Admin);
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
    }
}