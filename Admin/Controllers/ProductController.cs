using Microsoft.AspNetCore.Mvc;
using Common.Repositories;
using Common.Models;
using Common.Request;
using Common.Helper;

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

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] AddNewProduct product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl ?? "",
                IsDeleted = 0,
                CreatedTime = DateTime.Now
            };

            var result = await ProductRepositories.AddProduct(newProduct);

            if (!result)
            {
                return BadRequest(new { Message = "Can not add product" });
            }

            return Ok(new { Message = "Product added successfully" });
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
        public async Task<IActionResult> DeleteProduct([FromQuery] int productId)
        {
            if (productId <= 0)
            {
                return BadRequest("Product ID is required.");
            }

            ProductRepositories repository = new();

            var result = await repository.DeleteProduct(productId);

            if (!result)
            {
                return BadRequest(new { Message = "Can not updated product info" });
            }

            return Ok(new { Message = "Product deleted successfully" });
        }
    }
}