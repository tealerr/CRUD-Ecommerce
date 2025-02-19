using Microsoft.AspNetCore.Mvc;
using Common.Repositories;
using Common.Models;
using System.Threading.Tasks;

namespace Admin.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class ProductController : ControllerBase
    {
        // POST: api/admin/products
        [HttpPost]
        [Route("products")]
        public IActionResult GetProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            ProductRepositories repository = new();

            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var products = repository.GetProductList(pageNumber, pageSize);
            if (products == null || products.Count == 0)
            {
                return NotFound("No products found.");
            }

            return Ok(new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalProducts = products.Count,
                Products = products
            });
        }

        // GET: api/admin/{productId}
        [HttpGet("product/{productId}")]
        public IActionResult GetProductByID(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
            {
                return BadRequest("Product ID is required.");
            }

            ProductRepositories repository = new();

            var product = repository.GetProductByID(productId);

            if (product == null)
            {
                return NotFound($"Product with ID '{productId}' not found.");
            }

            return Ok(new
            {
                Product = product
            });
        }

        // PUT: api/admin/edit-product
        [HttpPut("edit-product")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            ProductRepositories repository = new();

            var result = await repository.UpdateProduct(product);

            if (!result)
            {
                return BadRequest(new { Message = "Can not updated product info" });
            }

            return Ok(new { Message = "Product updated successfully" });
        }

        // DELETE: api/product/delete
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int productId, sbyte IsDelete)
        {
            ProductRepositories repository = new();

            var result = await repository.DeleteProduct(productId, IsDelete);

            if (!result)
            {
                return BadRequest(new { Message = "Can not updated product info" });
            }

            return Ok(new { Message = "Product deleted successfully" });
        }
    }
}