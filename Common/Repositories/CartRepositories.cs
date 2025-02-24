using System.Diagnostics;
using Common.Helper;
using Common.Models;
using Common.Request;
using SqlKata.Execution;

namespace Common.Repositories
{
    public class CartRepositories
    {
        public async Task<bool> AddItemToCart(RequestAddItemToCart item)
        {
            try
            {
                var repository = new ProductRepositories();

                var product = repository.GetProductByID(item.ProductId) ?? throw new Exception("Product not found.");


                UserCartItem productToCart = new()
                {
                    ProductId = item.ProductId,
                    UserGuid = item.UserGUID,
                    Quantity = item.Quantity,
                    Price = product.Price,
                    Total = Math.Round(product.Price * item.Quantity, 2)
                };

                var connection = new DBConnection().Connect();
                var result = await connection.Query(Table.UserCartItem)
                .InsertAsync(new
                {
                    productToCart.UserGuid,
                    productToCart.ProductId,
                    productToCart.Price,
                    productToCart.Quantity,
                    productToCart.Total
                });
                connection.Connection.Close();

                if (result > 0)
                {
                    return true;
                }
                throw new Exception("Can not add product to cart.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public async Task<List<UserCartItem>?> GetUserItemInCart(string userGuid)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var result = await connection.Query(Table.UserCartItem)
                    .Where("UserGuid", userGuid)
                    .GetAsync<UserCartItem>();

                connection.Connection.Close();

                return result.ToList();

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public async Task<bool> RemoveItemFromCart(string userGUID, int productId)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var result = await connection.Query(Table.UserCartItem)
                    .Where(Column.UserGuid, userGUID)
                    .Where(Column.ProductId, productId)
                    .DeleteAsync();
                connection.Connection.Close();

                if (result > 0)
                {
                    return true;
                }
                throw new Exception("Can not remove product from cart.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public async Task<List<UserTransactionProduct>?> GetItemsInCart(int userTxId)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var result = await connection.Query(Table.UserTransactionProduct)
                    .Where("UserTransactionId", userTxId)
                    .GetAsync<UserTransactionProduct>();

                connection.Connection.Close();

                return result.ToList();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public async Task<double> CalculateItemsInCart(int userTxId)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var results = await connection.Query(Table.UserTransactionProduct)
                    .Where("UserTransactionId", userTxId)
                    .GetAsync<UserTransactionProduct>();

                connection.Connection.Close();

                if (!results.Any())
                {
                    throw new Exception($"Can not find product in cart of Transaction ID: ${userTxId}.");
                }

                double total = 0;

                foreach (var item in results)
                {
                    total += Math.Round(item.Quantity * item.Price, 2);
                }

                return total;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                throw new Exception("Can not calculate total price of items in cart.");
            }
        }

        public async Task<UserCartItem?> GetCartItemByProductId(string userGuid, int productId)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var result = await connection.Query(Table.UserCartItem)
                    .Where("UserGuid", userGuid)
                    .Where("ProductId", productId)
                    .FirstOrDefaultAsync<UserCartItem>();

                connection.Connection.Close();

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public async Task<bool> UpdateCartItem(UserCartItem item)
        {
            try
            {
                var connection = new DBConnection().Connect();

                var result = await connection.Query(Table.UserCartItem)
                    .Where("UserGuid", item.UserGuid)
                    .Where("ProductId", item.ProductId)
                    .UpdateAsync(new
                    {
                        item.Quantity,
                        Total = item.Price * item.Quantity
                    });

                connection.Connection.Close();

                return result > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}
