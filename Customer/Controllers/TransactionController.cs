using Common.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        [HttpGet("{transactionId}")]
        public IActionResult GetTransactionByID(int transactionId)
        {
            if (transactionId < 1)
            {
                return BadRequest("Transaction ID must be greater than zero.");
            }

            TransactionRepositories repository = new();

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
            TransactionRepositories repository = new();

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