using Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class TransactionController : ControllerBase
    {
        // POST: api/transaction/list
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

        // GET: api/transaction/{id}
        [HttpGet("transaction/{transactionId}")]
        public IActionResult GetTransactionByID(string transactionId)
        {
            if (string.IsNullOrWhiteSpace(transactionId))
            {
                return BadRequest("Product ID is required.");
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