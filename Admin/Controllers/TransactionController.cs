using Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        [HttpPost("transactions")]
        public IActionResult GetTransactions([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {

            TransactionRepositories repository = new();

            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var tx = repository.GetTransactionList(pageNumber, pageSize);
            if (tx == null || tx.Count == 0)
            {
                return NotFound("No transactions found.");
            }

            return Ok(new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalTransactions = tx.Count,
                Transactions = tx
            });
        }

        [HttpGet("{transactionId}")]
        public IActionResult GetTransactionByID(int transactionId)
        {
            if (transactionId.GetType() != typeof(int))
            {
                return BadRequest("Transaction ID must be an integer.");
            }

            if (transactionId < 1)
            {
                return BadRequest("Transaction ID is required.");
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
    }
}