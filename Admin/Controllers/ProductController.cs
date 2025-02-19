using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        // POST: api/product/list
        [HttpPost("list")]
        public IActionResult GetProducts()
        {
            return NoContent();
        }

        // GET: api/product/{id}
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            return NoContent();

        }

        // PUT: api/product/edit
        [HttpPut("edit")]
        public IActionResult EditProduct()
        {
            // This is just a placeholder. Replace with actual logic to edit a product.
            return NoContent();
        }

        // DELETE: api/product/delete/{id}
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            // This is just a placeholder. Replace with actual logic to delete a product.
            return Ok(new { Message = "Product deleted successfully" });
        }
    }
}