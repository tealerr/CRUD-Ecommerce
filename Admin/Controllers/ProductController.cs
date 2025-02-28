using Microsoft.AspNetCore.Mvc;
using Common.Repositories;
using Common.Models;
using Common.Request;
using Common.Helper;
using SixLabors.ImageSharp;
using Microsoft.AspNetCore.Identity;

namespace Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManagerService;

        public ProductController(UserManager<IdentityUser> userManagerService)
        {
            _userManagerService = userManagerService;
        }

        [HttpPost]
        [Route("products")]
        public async Task<IActionResult> GetProducts(
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
        public async Task<IActionResult> GetProductByID([FromHeader(Name = "Authorization")] string bearerToken, int productId)
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
        public async Task<IActionResult> AddProduct([FromHeader(Name = "Authorization")] string bearerToken, [FromForm] AddNewProduct product)
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
        public async Task<IActionResult> UpdateProduct([FromHeader(Name = "Authorization")] string bearerToken, [FromBody] UpdateProduct product)
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

            ProductRepositories repository = new();

            var result = await repository.UpdateProduct(product);

            if (!result)
            {
                return BadRequest(new { Message = "Can not updated product info" });
            }

            return Ok(new { Message = "Product updated successfully" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProduct([FromHeader(Name = "Authorization")] string bearerToken, [FromQuery] int productId)
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

