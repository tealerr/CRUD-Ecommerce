using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        // POST: api/transaction/list
        [HttpPost("list")]
        public IActionResult GetTransactions()
        {

            return NoContent();
        }

        // GET: api/transaction/{id}
        [HttpGet("{id}")]
        public IActionResult GetTransaction(int id)
        {
            return NoContent();
        }
    }
}