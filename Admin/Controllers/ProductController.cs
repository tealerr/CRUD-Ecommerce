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
                        string imageUrl = ImageHelper.GetImageUrl(product.ImageUrl, HttpContext);
                        product.ImageUrl = imageUrl;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error generating image URL for product {product.Id}: {ex.Message}");
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
                    string imageUrl = ImageHelper.GetImageUrl(product.ImageUrl, HttpContext);
                    product.ImageUrl = imageUrl;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error generating image URL for product {product.Id}: {ex.Message}");
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
            string savedImagePath = "";

            if (product.Image != null)
            {
                using var ms = new MemoryStream();

                await product.Image.CopyToAsync(ms);
                byte[] imageBytes = ms.ToArray();

                if (imageBytes.Length > 0)
                {
                    string fileExtension = Path.GetExtension(product.Image.FileName).ToLower();
                    if (fileExtension == ".png")
                    {
                        savedImagePath = ImageHelper.SaveImage(base64String, "/Uploads", ImageHelper.FileType.Png);
                    }
                    else if (fileExtension == ".jpg")
                    {
                        base64String = Convert.ToBase64String(imageBytes);
                        savedImagePath = ImageHelper.SaveImage(base64String, "/Uploads", ImageHelper.FileType.JPeg);
                    }
                    else if (fileExtension == ".jpeg")
                    {
                        base64String = Convert.ToBase64String(imageBytes);
                        savedImagePath = ImageHelper.SaveImage(base64String, "/Uploads", ImageHelper.FileType.JPeg);
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
                ImageUrl = savedImagePath,
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

