using System.IdentityModel.Tokens.Jwt;
using Common.Repositories;
using Common.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "LoginPolicy")]
    public class CartController : ControllerBase
    {
        [HttpPost("add-items")]
        public async Task<IActionResult> AddItemToCart([FromBody] RequestAddItemToCart item, [FromHeader(Name = "Authorization")] string bearerToken)
        {
            if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer "))
            {
                return Unauthorized("Invalid or missing token.");
            }

            var token = bearerToken["Bearer ".Length..].Trim();
            var handler = new JwtSecurityTokenHandler();

            if (handler.ReadToken(token) is not JwtSecurityToken jwtToken)
            {
                return Unauthorized("Invalid token.");
            }

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var userId = userIdClaim.Value;
            var userInfos = UserRepositories.GetUserById(userId);
            if (userInfos == null)
            {
                return NotFound($"User with ID '{userId}' not found.");
            }

            CartRepositories repository = new();

            var existingItem = await repository.GetCartItemByProductId(userInfos.UserGuid, item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                existingItem.Total = existingItem.Price * existingItem.Quantity;
                var updateResult = await repository.UpdateCartItem(existingItem);

                if (!updateResult)
                {
                    return BadRequest("Failed to update item quantity in cart.");
                }

                return Ok(new { results = "Item quantity updated in cart." });
            }

            var result = await repository.AddItemToCart(item, userInfos.UserGuid);

            if (!result)
            {
                return BadRequest("Failed to add item to cart.");
            }

            return Ok(new { results = "Item added to cart." });
        }


        [HttpPut("update-items")]
        public async Task<IActionResult> UpdateItemInCart([FromBody] RequestAddItemToCart item, [FromHeader(Name = "Authorization")] string bearerToken)
        {
            if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer "))
            {
                return Unauthorized("Invalid or missing token.");
            }

            var token = bearerToken["Bearer ".Length..].Trim();
            var handler = new JwtSecurityTokenHandler();

            if (handler.ReadToken(token) is not JwtSecurityToken jwtToken)
            {
                return Unauthorized("Invalid token.");
            }

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var userId = userIdClaim.Value;
            var userInfos = UserRepositories.GetUserById(userId);
            if (userInfos == null)
            {
                return NotFound($"User with ID '{userId}' not found.");
            }

            CartRepositories repository = new();

            var existingItem = await repository.GetCartItemByProductId(userInfos.UserGuid, item.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
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
                return BadRequest("Item not found in cart.");
            }
        }

        [HttpGet("get-items")]
        public async Task<IActionResult> GetUserItemInCart([FromHeader(Name = "Authorization")] string bearerToken)
        {
            if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer "))
            {
                return Unauthorized("Invalid or missing token.");
            }

            var token = bearerToken["Bearer ".Length..].Trim();
            var handler = new JwtSecurityTokenHandler();

            if (handler.ReadToken(token) is not JwtSecurityToken jwtToken)
            {
                return Unauthorized("Invalid token.");
            }

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var userId = userIdClaim.Value;
            var userInfos = UserRepositories.GetUserById(userId);
            if (userInfos == null)
            {
                return NotFound($"User with ID '{userId}' not found.");
            }

            CartRepositories repository = new();

            var result = await repository.GetUserItemInCart(userInfos.UserGuid);

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
        [HttpPost]
        [Route("submit")]
        public async Task<IActionResult> SubmitTransaction([FromBody] RequestSubmitTransactions request, [FromHeader(Name = "Authorization")] string bearerToken)
        {
            if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer "))
            {
                return Unauthorized("Invalid or missing token.");
            }

            var token = bearerToken["Bearer ".Length..].Trim();
            var handler = new JwtSecurityTokenHandler();

            if (handler.ReadToken(token) is not JwtSecurityToken jwtToken)
            {
                return Unauthorized("Invalid token.");
            }

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var userId = userIdClaim.Value;
            var userInfos = UserRepositories.GetUserById(userId);
            if (userInfos == null)
            {
                return NotFound($"User with ID '{userId}' not found.");
            }

            if (request == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            if (request.Transaction.Count == 0)
            {
                return BadRequest("Transaction items cannot be null or empty.");
            }

            double grandTotal = 0;
            foreach (var item in request.Transaction)
            {
                var product = ProductRepositories.GetProductByID(item.ProductId);
                if (product == null)
                {
                    return NotFound($"Product with ID '{item.ProductId}' not found.");
                }

                double total = Math.Round(product.Price * item.Quantity, 2);
                grandTotal += total;
            }

            int? userTransactionId = TransactionRepositories.CreateUserTransaction(userInfos.UserGuid, grandTotal);
            if (userTransactionId == null)
            {
                Console.Error.WriteLine("Failed to create user transaction.");
                return BadRequest("Failed to create user transaction.");
            }

            foreach (var item in request.Transaction)
            {
                var result = await TransactionRepositories.SubmitTransaction(item, userTransactionId.Value, userInfos.UserGuid);

                if (!result)
                {
                    return BadRequest("Failed to submit transaction.");
                }
            }

            return Ok(new
            {
                results = "Transaction submitted successfully."
            });
        }

        // delete item from cart
        [HttpDelete("delete-item")]
        public async Task<IActionResult> DeleteItem([FromQuery] int productId, [FromHeader(Name = "Authorization")] string bearerToken)
        {
            if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer "))
            {
                return Unauthorized("Invalid or missing token.");
            }

            var token = bearerToken["Bearer ".Length..].Trim();
            var handler = new JwtSecurityTokenHandler();

            if (handler.ReadToken(token) is not JwtSecurityToken jwtToken)
            {
                return Unauthorized("Invalid token.");
            }

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var userId = userIdClaim.Value;
            var userInfos = UserRepositories.GetUserById(userId);
            if (userInfos == null)
            {
                return NotFound($"User with ID '{userId}' not found.");
            }
            var result = await CartRepositories.RemoveItemFromCart(userInfos.UserGuid, productId);

            if (!result)
            {
                return BadRequest("Failed to delete item from cart.");
            }

            return Ok(new { results = "Item deleted from cart." });
        }
    }
}