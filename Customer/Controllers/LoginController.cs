using Common.Helper;
using Common.Models;
using Common.Models.Utilities;
using Common.Repositories;
using Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Customer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LoginController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("login")]
        [Authorize(Policy = "ApiPolicy")]
        public async Task<ActionResult<dynamic>> LoginCustomer([FromBody] InternalLogin input)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(input.Email);
                if (user == null)
                {
                    return new BaseResponse().Fail(null, "InvalidUserPassword");
                }

                var result = await _signInManager.PasswordSignInAsync(user, input.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var status = new UserRepositories().GetStatusAspnetuserByEmail(input.Email);
                    if (status == 0)
                    {
                        return new BaseResponse().Fail(null, "Username Cannot access this site");
                    }

                    if (user == null) return new BaseResponse().Fail(null, "InvalidUserPassword");

                    var role = await _userManager.GetRolesAsync(user);
                    if (role.Contains(RoleHelper.User))
                    {
                        var token = UserHelper.GenerateToken(user);
                        if (user.UserName != null)
                        {
                            new UserRepositories().InsertAspnetusertokens(user.Id, CodeHelper.TOKEN_PROVIDER_USER_SITE, user.UserName, token);
                        }
                        else
                        {
                            return new BaseResponse().Fail(null, "InvalidUserName");
                        }
                        var permission = new PermissionRepository().GetPermissionList(user.Id);

                        return new BaseResponse().Success(new
                        {
                            user_guid = user.Id,
                            token,
                            EMAIL = user.Email,
                            user.UserName,
                            Permission = permission,
                        });
                    }

                }
                else
                {
                    Console.WriteLine("Failed to sign in.");
                    if (result.IsLockedOut)
                    {
                        return new BaseResponse().Fail("User account is locked out.");
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return new BaseResponse().Fail("Two-factor authentication required.");
                    }
                    return new BaseResponse().Fail(null, "InvalidUserPassword");
                }

                return new BaseResponse().Fail(null, "InvalidUserPassword");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);

                return new BaseResponse().Fail($"Login Failed: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("logout")]
        [Authorize(Policy = "ApiPolicy")]
        public async Task<ActionResult<dynamic>> LogoutUser()
        {
            try
            {
                var context = new CodeHelperModel(HttpContext);
                if (!string.IsNullOrEmpty(context.UserId))
                {
                    var user = await _userManager.FindByIdAsync(context.UserId);
                    if (user != null)
                    {
                        await HttpContext.SignOutAsync();

                        return new BaseResponse().Success("Logout Success");
                    }
                }

                return new BaseResponse().Fail("Logout Failed");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);

                return new BaseResponse().Fail($"Logout Failed: {ex.Message}");
            }
        }
    }

}
