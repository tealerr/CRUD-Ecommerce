using Microsoft.AspNetCore.Mvc;
using Common.Repositories;
using Common.Models;
using Common.Request;
using Common.Helper;
using SixLabors.ImageSharp;

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

            foreach (var product in products)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    try
                    {
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", $"{product.Id}.png");

                        if (!System.IO.File.Exists(filePath))
                        {
                            byte[] imageBytes = Convert.FromBase64String(product.ImageUrl);
                            System.IO.File.WriteAllBytes(filePath, imageBytes);
                        }

                        product.ImageUrl = ImageHelper.GetImageUrlFromPath(filePath);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Error decoding Base64 for product {product.Id}: {ex.Message}");
                        product.ImageUrl = "";
                    }
                }
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

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                try
                {
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", $"{product.Id}.png");
                    if (ReadFileHelper.IsValidBase64(product.ImageUrl))
                    {
                        if (!System.IO.File.Exists(filePath))
                        {
                            byte[] imageBytes = Convert.FromBase64String(product.ImageUrl);
                            System.IO.File.WriteAllBytes(filePath, imageBytes);
                        }

                        product.ImageUrl = ImageHelper.GetImageUrlFromPath(filePath);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid Base64 format for product {product.Id}");
                        product.ImageUrl = "";
                    }

                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error decoding Base64 for product {product.Id}: {ex.Message}");
                    product.ImageUrl = "";
                }
            }

            return Ok(new
            {
                Product = product
            });
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromForm] AddNewProduct product)
        {
            string base64String = "";

            if (product.Image != null)
            {
                using var ms = new MemoryStream();

                await product.Image.CopyToAsync(ms);
                byte[] imageBytes = ms.ToArray();

                if (imageBytes.Length > 0)
                {
                    string fileExtension = Path.GetExtension(product.Image.FileName).ToLower();

                    if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".jpeg")
                    {
                        base64String = Convert.ToBase64String(imageBytes);
                    }
                    else
                    {
                        return BadRequest(new { Message = "Unsupported file type. Please upload a PNG, JPG, or WEBP image." });
                    }
                }
            }
            else
            {
                return BadRequest(new { Message = "No image file uploaded." });
            }

            var newProduct = new Product
            {
                Name = product.Name,
                Price = product.Price,
                ImageUrl = base64String,
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