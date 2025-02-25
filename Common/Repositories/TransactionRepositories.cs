using System.Diagnostics;
using System.Transactions;
using Common.Helper;
using Common.Models;
using Common.Request;
using Common.Responses;
using SqlKata.Execution;

namespace Common.Repositories
{
    public class TransactionRepositories
    {
        public List<UserTransaction>? GetTransactionList(int pageNumber, int pageSize)
        {
            try
            {
                using (var connection = new DBConnection().Connect())
                {
                    var transactions = connection.Query(Table.UserTransaction)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .Get<UserTransaction>()
                        .ToList();

                    if (transactions.Count == 0)
                    {
                        Debug.WriteLine("No transactions found.");
                        return null;
                    }

                    return transactions;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error finding transaction list: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);

                return null;
            }
        }

        public UserTransaction? GetAllUserTransactionByID(int txId)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var transaction = connection.Query(Table.UserTransaction)
                                   .Where(Column.Id, txId)
                                   .FirstOrDefault<UserTransaction>();
                connection.Connection.Close();

                if (transaction == null)
                {
                    Debug.WriteLine($"Transaction with ID: {txId} not found");
                    return null;
                }

                return transaction;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error finding transaction with ID: {txId}: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);

                return null;
            }
        }

        public TransactionResponse? GetUserTransactionByID(int txId, string userGUID)
        {
            try
            {
                var connection = new DBConnection().Connect();

                var transaction = connection.Query(Table.UserTransaction)
                                    .Select($"{Table.UserTransaction}.{Column.Id} as UserTransactionId",
                                            $"{Table.UserTransaction}.{Column.GrandTotal}",
                                            $"{Table.UserTransaction}.{Column.CreatedTime}")
                                    .Where($"{Table.UserTransaction}.{Column.Id}", txId)
                                    .Where($"{Table.UserTransaction}.{Column.UserGuid}", userGUID)
                                    .FirstOrDefault<TransactionResponse>();

                if (transaction == null)
                {
                    Debug.WriteLine($"Transaction with ID: {txId} not found");
                    connection.Connection.Close();
                    return null;
                }

                var transactionDetails = connection.Query(Table.UserTransactionProduct)
                                        .Select($"{Table.Product}.{Column.Name} as ProductName",
                                                $"{Table.Product}.{Column.Price} as ProductPrice",
                                                $"{Table.UserTransactionProduct}.{Column.Quantity}",
                                                $"{Table.UserTransactionProduct}.{Column.Total}")
                                        .Join(Table.Product, $"{Table.Product}.{Column.Id}", $"{Table.UserTransactionProduct}.{Column.ProductId}")
                                        .Where($"{Table.UserTransactionProduct}.UserTransactionId", txId)
                                        .Get<UserTransactionProductResponse>()
                                        .ToList();

                transaction.TransactionDetails = transactionDetails;
                connection.Connection.Close();

                return transaction;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error finding transaction with ID: {txId}: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);

                return null;
            }
        }

        public static int? CreateUserTransaction(string UserGUID, double grandTotal)
        {
            try
            {
                var newTransaction = new UserTransaction
                {
                    UserGuid = UserGUID,
                    GrandTotal = grandTotal,
                    CreatedTime = DateTime.Now
                };

                using var connection = new DBConnection().Connect();
                int result = connection.Query(Table.UserTransaction)
                    .InsertGetId<int>(new
                    {
                        newTransaction.UserGuid,
                        newTransaction.GrandTotal,
                        newTransaction.CreatedTime
                    });

                if (result == 0)
                {
                    Debug.WriteLine("Failed to create transaction.");
                    return null;
                }

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while creating transaction: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);

                return null;
            }
        }

        public static async Task<bool> SubmitTransaction(RequestSubmitTransaction transaction, int userTransactionId, string userGUID)
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted
                };
                using var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                using var connection = new DBConnection().Connect();

                var newProductTransaction = new UserTransactionProduct
                {
                    UserTransactionId = userTransactionId,
                    ProductId = transaction.ProductId,
                    Price = transaction.Price,
                    Quantity = transaction.Quantity,
                    Total = Math.Round(transaction.Price * transaction.Quantity, 2)
                };

                var result = await connection.Query(Table.UserTransactionProduct)
                    .InsertAsync(new
                    {
                        newProductTransaction.UserTransactionId,
                        newProductTransaction.ProductId,
                        newProductTransaction.Price,
                        newProductTransaction.Quantity,
                        newProductTransaction.Total
                    });

                if (result == 0)
                {
                    Console.Error.WriteLine("Failed to insert user transaction.");
                    return false;
                }

                var deleteResult = await CartRepositories.RemoveItemFromCart(userGUID, transaction.ProductId);
                if (!deleteResult)
                {
                    Console.Error.WriteLine("Failed to remove item from cart.");
                    return false;
                }

                scope.Complete();

                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error submitting transaction: {ex.Message}");
                Console.Error.WriteLine(ex.StackTrace);

                return false;
            }
        }

    }
}
