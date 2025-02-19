using System.Diagnostics;
using Common.Helper;
using Common.Models;
using SqlKata.Execution;

namespace Common.Repositories
{
    public class ProductRepositories
    {

        public Product? GetProductByID(string productID)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var productInfos = connection.Query(Table.Product)
                                   .Where(Column.ProductId, productID)
                                   .Where(Column.IsDelete, false)
                                   .AsCount()
                                   .FirstOrDefault<Product>();
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
                Debug.WriteLine($"Error finding product with ID {productID}: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);

                return null;
            }
        }

        public List<Product>? GetProductList(int pageNumber, int pageSize)
        {
            try
            {
                using (var connection = new DBConnection().Connect())
                {
                    var productList = connection.Query(Table.Product)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .Where(Column.IsDelete, false)
                        .Get<Product>()
                        .ToList();

                    if (productList.Count == 0)
                    {
                        Debug.WriteLine("No products found.");
                        return null;
                    }

                    return productList;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error finding product list: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);

                return null;
            }
        }

        public async Task<bool> UpdateProduct(Product productToUpdate)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var productInfos = await connection.Query(Table.Product)
                .Where(Column.ProductId, productToUpdate.Id)
                .Where(Column.IsDelete, false)
                .FirstOrDefaultAsync<Product>() ?? throw new Exception("Product not found");

                Product updatedProduct = new()
                {
                    Name = productToUpdate.Name == "" ? productInfos.Name : productToUpdate.Name,
                    Price = productToUpdate.Price == "" ? productInfos.Price : productToUpdate.Price,
                    ImageUrl = productToUpdate.ImageUrl == "" ? productInfos.ImageUrl : productToUpdate.ImageUrl,
                    IsDeleted = (sbyte)(productToUpdate.IsDeleted != 0 ? productInfos.IsDeleted : productToUpdate.IsDeleted),
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
                .Where(Column.ProductId, productId)
                .Where(Column.IsDelete, false)
                .FirstOrDefaultAsync<Product>() ?? throw new Exception("Product not found");

                Product updatedProduct = new()
                {
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
