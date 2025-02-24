using Common.Models;
using Common.Repositories;
using Common.Request;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {

        [HttpPost("add-items")]
        public async Task<IActionResult> AddItemToCart([FromBody] RequestAddItemToCart item)
        {
            CartRepositories repository = new();

            // Check if the item already exists in the cart
            var existingItem = await repository.GetCartItemByProductId(item.UserGUID, item.ProductId);

            if (existingItem != null)
            {
                // Update the quantity of the existing item
                existingItem.Quantity = item.Quantity;
                existingItem.Total = existingItem.Price * existingItem.Quantity;
                var updateResult = await repository.UpdateCartItem(existingItem);

                if (!updateResult)
                {
                    return BadRequest("Failed to update item quantity in cart.");
                }

                return Ok(new { results = "Item quantity updated in cart." });
            }
            else
            {
                // Add new item to the cart
                var result = await repository.AddItemToCart(item);

                if (!result)
                {
                    return BadRequest("Failed to add item to cart.");
                }

                return Ok(new { results = "Item added to cart." });
            }
        }

        [HttpGet("get-items/{userGuid}")]
        public async Task<IActionResult> GetUserItemInCart(string userGuid)
        {
            CartRepositories repository = new();

            var result = await repository.GetUserItemInCart(userGuid);

            return Ok(new { results = result });
        }

        // POST: api/cart/calculate
        [HttpPost("calculate/{transactionId}")]
        public async Task<IActionResult> Calculate(int transactionId)
        {
            CartRepositories repository = new();

            // Calculate the total price of the cart
            var totalPrice = await repository.CalculateItemsInCart(transactionId);

            return Ok(new { results = totalPrice });
        }

        // POST: api/cart/submit
        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromBody] UserTransactionProduct item)
        {
            CartRepositories repository = new();

            // var result = await repository.AddItemToCart(item);

            // if (!result)
            // {
            //     return BadRequest("Failed to add item to cart.");
            // }

            return Ok(new { results = "Item added to cart." });
        }

        // delete item from cart
        [HttpDelete("delete-item")]
        public async Task<IActionResult> DeleteItem([FromQuery] string userGUID, int productId)
        {
            CartRepositories repository = new();

            var result = await repository.RemoveItemFromCart(userGUID, productId);

            if (!result)
            {
                return BadRequest("Failed to delete item from cart.");
            }

            return Ok(new { results = "Item deleted from cart." });
        }
    }
}