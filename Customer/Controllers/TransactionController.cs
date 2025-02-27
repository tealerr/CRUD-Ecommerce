using Common.Helper;
using Common.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        [HttpGet("{transactionId}")]
        [Authorize(Policy = "LoginPolicy")]
        public IActionResult GetUserTransactionByID(int transactionId, [FromHeader(Name = "Authorization")] string bearerToken)
        {
            if (transactionId < 1)
            {
                return BadRequest("Transaction ID must be greater than zero.");
            }

            if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer "))
            {
                return Unauthorized("Invalid or missing token.");
            }

            var userGuid = TokenHelper.GetUserGuidFromToken(bearerToken);
            if (userGuid == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            TransactionRepositories repository = new();
            var transaction = repository.GetUserTransactionByID(transactionId, userGuid);

            if (transaction == null)
            {
                return NotFound($"Transaction with ID '{transactionId}' not found.");
            }

            return Ok(new
            {
                Results = transaction
            });
        }

        [HttpPost]
        [Route("transactions")]
        [Authorize(Policy = "ApiPolicy")]
        public IActionResult GetUserTransactions([FromHeader(Name = "Authorization")] string bearerToken, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            TransactionRepositories repository = new();

            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer "))
            {
                return Unauthorized("Invalid or missing token.");
            }

            var userGuid = TokenHelper.GetUserGuidFromToken(bearerToken);
            if (userGuid == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var transactions = TransactionRepositories.GetUserTransactionList(pageNumber, pageSize, userGuid);

            if (transactions == null || transactions.Count == 0)
            {
                return NotFound("No transactions found.");
            }

            return Ok(new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalTransactions = transactions.Count,
                Transactions = transactions
            });
        }

    }
}