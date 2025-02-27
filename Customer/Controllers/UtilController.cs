using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Customer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UtilController : ControllerBase
    {
        private readonly IPasswordHasher<IdentityUser> _passwordHasher;

        public UtilController(IPasswordHasher<IdentityUser> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        [HttpPost("hash")]
        public IActionResult HashPassword([FromBody] PasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Password cannot be empty");
            }

            var user = new IdentityUser(); // ไม่ต้องตั้งค่าอะไรมากสำหรับ Hash
            var hashedPassword = _passwordHasher.HashPassword(user, request.Password);

            return Ok(new { HashedPassword = hashedPassword });
        }
    }

    public class PasswordRequest
    {
        public string Password { get; set; } = string.Empty;
    }

}


