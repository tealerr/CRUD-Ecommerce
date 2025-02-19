using System.Diagnostics;
using Common.Helper;
using Common.Models;
using SqlKata.Execution;

namespace Common.Repositories
{
    public class CartRepositories
    {
        public async Task<bool> AddItemToCart(UserTransactionProduct newProduct)
        {
            try
            {
                int userTxProductId = Guid.NewGuid().GetHashCode();

                UserTransactionProduct productToCart = new UserTransactionProduct
                {
                    Id = newProduct.Id,
                    UserTransactionId = userTxProductId,
                    Quantity = newProduct.Quantity,
                    Price = newProduct.Price,
                    Total = (double)(newProduct.Quantity * newProduct.Price)
                };

                var connection = new DBConnection().Connect();
                var result = await connection.Query(Table.UserTransactionProduct)
                .InsertAsync(productToCart);
                connection.Connection.Close();

                if (result > 0)
                {
                    return true;
                }
                throw new Exception("Can not add product to cart.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public async Task<bool> RemoveItemFromCart(int userTxId, int itemId)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var result = await connection.Query(Table.UserTransactionProduct)
                    .Where("UserTransactionId", userTxId)
                    .Where("Id", itemId)
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
    }
}
