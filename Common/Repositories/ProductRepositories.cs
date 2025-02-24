using System.Diagnostics;
using Common.Helper;
using Common.Models;
using Common.Request;
using SqlKata.Execution;

namespace Common.Repositories
{
    public class ProductRepositories
    {

        public Product? GetProductByID(int productID)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var productInfos = connection.Query(Table.Product)
                                   .Where(Column.Id, productID)
                                   .Where(Column.IsDeleted, CodeHelper.FALSE)
                                   .FirstOrDefault<Product>();
                Console.WriteLine("productInfos:: ", productInfos);

                connection.Connection.Close();

                if (productInfos == null)
                {
                    Debug.WriteLine($"Product with ID {productID} not found");

                    return null;
                }

                return productInfos;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error finding product with ID {productID}: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);

                return null;
            }
        }

        public List<Product>? GetProductList(int pageNumber, int pageSize)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var productList = connection.Query(Table.Product)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Where(Column.IsDeleted, CodeHelper.FALSE)
                    .Get<Product>()
                    .ToList();
                connection.Connection.Close();

                Console.WriteLine($"productList:: {productList}");

                if (productList.Count == 0)
                {
                    Debug.WriteLine("No products found.");
                    return null;
                }

                return productList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error finding product list: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);
                Console.Error.WriteLine($"Error finding product list: {ex.Message}");

                return null;
            }
        }

        public async Task<bool> UpdateProduct(UpdateProduct productToUpdate)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var productInfos = await connection.Query(Table.Product)
                .Where(Column.Id, productToUpdate.Id)
                .Where(Column.IsDeleted, CodeHelper.FALSE)
                .FirstOrDefaultAsync<Product>() ?? throw new Exception("Product not found");

                Product updatedProduct = new()
                {
                    Id = productInfos.Id,
                    Name = productToUpdate.Name == null || productToUpdate.Name == "" ? productInfos.Name : productToUpdate.Name,
                    Price = productToUpdate.Price ?? productInfos.Price,
                    ImageUrl = string.IsNullOrEmpty(productToUpdate.ImageUrl) ? productInfos.ImageUrl : productToUpdate.ImageUrl,
                    IsDeleted = productInfos.IsDeleted,
                    CreatedTime = productInfos.CreatedTime
                };

                // Apply update
                await connection.Query(Table.Product)
                .Where(Column.Id, productToUpdate.Id)
                .UpdateAsync(updatedProduct);

                connection.Connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updated product: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);

                return false;
            }
        }

        public async Task<bool> DeleteProduct(int productId, sbyte IsDelete)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var productInfos = await connection.Query(Table.Product)
                .Where(Column.Id, productId)
                .Where(Column.IsDeleted, false)
                .FirstOrDefaultAsync<Product>() ?? throw new Exception("Product not found");

                Product updatedProduct = new()
                {
                    Id = productInfos.Id,
                    Name = productInfos.Name,
                    Price = productInfos.Price,
                    ImageUrl = productInfos.ImageUrl,
                    CreatedTime = productInfos.CreatedTime,
                    IsDeleted = IsDelete
                };

                // Apply update
                await connection.Query(Table.Product)
                .Where(Column.Id, productId)
                .UpdateAsync(updatedProduct);

                connection.Connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updated product: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);

                return false;
            }
        }

    }
}
