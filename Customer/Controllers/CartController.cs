using Microsoft.AspNetCore.Mvc;

namespace Customer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        // POST: api/cart/calculate
        [HttpPost("calculate")]
        public IActionResult Calculate()
        {
            return Ok();
        }

        // POST: api/cart/submit
        [HttpPost("submit")]
        public IActionResult Submit()
        {
            return Ok(new { Message = "Cart submitted successfully" });
        }
    }
}