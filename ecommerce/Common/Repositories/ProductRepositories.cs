using System.Diagnostics;
using Common.Helper;
using ecommerce.Models;
using Ecommerce.Controllers.Admin;
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

    }
}
