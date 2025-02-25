using Common.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ApiPolicy")]
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

            var product = ProductRepositories.GetProductByID(productId);

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