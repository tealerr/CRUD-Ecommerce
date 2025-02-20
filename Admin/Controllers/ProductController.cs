using Microsoft.AspNetCore.Mvc;
using Common.Repositories;
using Common.Models;
using Common.Request;

namespace Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
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

        [HttpGet("{productId}")]
        public IActionResult GetProductByID(int productId)
        {
            if (productId <= 0)
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

        [HttpPut("edit-product")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct product)
        {
            ProductRepositories repository = new();

            var result = await repository.UpdateProduct(product);

            if (!result)
            {
                return BadRequest(new { Message = "Can not updated product info" });
            }

            return Ok(new { Message = "Product updated successfully" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int productId, sbyte IsDelete)
        {
            if (productId <= 0)
            {
                return BadRequest("Product ID is required.");
            }
            if (IsDelete < 0 || IsDelete > 1)
            {
                return BadRequest("IsDelete must be 0 or 1.");
            }

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