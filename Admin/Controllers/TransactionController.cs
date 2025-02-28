using Common.Helper;
using Common.Repositories;
using Common.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManagerService;

        public TransactionController(UserManager<IdentityUser> userManagerService)
        {
            _userManagerService = userManagerService;
        }

        [HttpPost("transactions")]
        public async Task<IActionResult> GetTransactions(
        [FromHeader(Name = "Authorization")] string bearerToken,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
            if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer "))
            {
                return Unauthorized("Invalid or missing token.");
            }

            var userGuid = TokenHelper.GetUserGuidFromToken(bearerToken);
            if (userGuid == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            if (_userManagerService == null)
            {
                return StatusCode(500, "User manager not found.");
            }

            RoleHelper roleHelper = new(_userManagerService);
            bool isAdmin = await roleHelper.IsAdmin(userGuid);

            if (!isAdmin)
            {
                return Unauthorized("User is not authorized to access this resource.");
            }

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
        public async Task<IActionResult> GetTransactionByID(
        [FromHeader(Name = "Authorization")] string bearerToken,
        int transactionId)
        {
            if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer "))
            {
                return Unauthorized("Invalid or missing token.");
            }

            var userGuid = TokenHelper.GetUserGuidFromToken(bearerToken);
            if (userGuid == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            if (_userManagerService == null)
            {
                return StatusCode(500, "User manager not found.");
            }

            RoleHelper roleHelper = new(_userManagerService);
            bool isAdmin = await roleHelper.IsAdmin(userGuid);

            if (!isAdmin)
            {
                return Unauthorized("User is not authorized to access this resource.");
            }

            if (transactionId.GetType() != typeof(int))
            {
                return BadRequest("Transaction ID must be an integer.");
            }

            if (transactionId < 1)
            {
                return BadRequest("Transaction ID is required.");
            }

            TransactionRepositories repository = new();

            var transaction = repository.GetAllUserTransactionByID(transactionId);

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