using Common.Models;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Controllers
{
    [ApiController]
    [Route("api/customer/[controller]")]
    public class CartController : ControllerBase
    {
        // POST: api/cart/calculate
        [HttpPost("calculate/{transactionId}")]
        public async Task<IActionResult> Calculate(int transactionId)
        {
            CartRepositories repository = new CartRepositories();

            // Calculate the total price of the cart
            var totalPrice = await repository.CalculateItemsInCart(transactionId);

            return Ok(new { results = totalPrice });
        }

        // POST: api/cart/submit
        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromBody] UserTransactionProduct item)
        {
            CartRepositories repository = new CartRepositories();

            var result = await repository.AddItemToCart(item);

            if (!result)
            {
                return BadRequest("Failed to add item to cart.");
            }

            return Ok(new { results = "Item added to cart." });
        }
    }
}