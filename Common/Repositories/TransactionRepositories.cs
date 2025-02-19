using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Transactions;
using Common;
using Common.Helper;
using Common.Models;
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
                Debug.WriteLine($"Error finding product list: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);

                return null;
            }
        }

        public UserTransaction? GetUserTransactionByID(string txId)
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

    }
}
