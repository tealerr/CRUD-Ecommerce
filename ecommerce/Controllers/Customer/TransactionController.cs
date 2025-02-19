using ecommerce.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ecommerce.Controllers.Customer
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        [HttpGet("get-transaction-by-id/{transactionId}")]
        public IActionResult GetTransactionByID(string transactionId)
        {
            if (string.IsNullOrWhiteSpace(transactionId))
            {
                return BadRequest("Product ID is required.");
            }

            TransactionRepositories repository = new TransactionRepositories();

            var transaction = repository.GetUserTransactionByID(transactionId);

            if (transaction == null)
            {
                return NotFound($"Transaction with ID '{transactionId}' not found.");
            }

            return Ok(new
            {
                results = transaction
            });
        }

        [HttpPost]
        [Route("transactions")]
        public IActionResult GetTransactions([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            TransactionRepositories repository = new TransactionRepositories();

            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var transactions = repository.GetTransactionList(pageNumber, pageSize);

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