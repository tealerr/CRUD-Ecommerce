using Common.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        [Route("products")]
        public IActionResult GetProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            ProductRepositories repository = new ProductRepositories();

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

        [HttpGet("get-product-by-id/{productId}")]
        public IActionResult GetProductByID(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
            {
                return BadRequest("Product ID is required.");
            }

            ProductRepositories repository = new ProductRepositories();

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
    }
}