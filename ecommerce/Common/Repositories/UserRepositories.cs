using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Common.Helper;
using ecommerce.Models;
using SqlKata.Execution;

namespace Common.Repositories
{
    public class UserRepositories
    {

        public bool ValidateUserGUID(string guid)
        {
            try
            {
                var connection = new DbConnection().Connect();
                var result = connection.Query(Table.User)
                                   .Where(Column.UserGuid, guid)
                                   .AsCount()
                                   .FirstOrDefault<int>();
                connection.Connection.Close();
                if (result > 0)
                {
                    result = false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var result = await connection.Query(Table.User)
                .InsertAsync(user);

                connection.Connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var userInfos = await connection.Query(Table.User)
                .Where(Column.UserGuid, user.UserGuid)
                .FirstOrDefaultAsync<User>();

                if (userInfos == null)
                {
                    throw new Exception("User not found.");
                }

                User updatedUserInfos = new User
                {
                    Firstname = string.IsNullOrEmpty(user.Firstname) ? userInfos.Firstname : user.Firstname,
                    Lastname = string.IsNullOrEmpty(user.Lastname) ? userInfos.Lastname : user.Lastname,
                    Nickname = string.IsNullOrEmpty(user.Nickname) ? userInfos.Nickname : user.Nickname,
                };

                // Apply update
                await connection.Query(Table.User)
                .Where(Column.UserGuid, user.UserGuid)
                .UpdateAsync(updatedUserInfos);

                connection.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating user: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public List<User>? GetUserList(int pageNumber, int pageSize)
        {
            try
            {
                using (var connection = new DBConnection().Connect())
                {
                    var userList = connection.Query(Table.User)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .Get<User>()
                        .ToList();

                    if (userList.Count == 0)
                    {
                        Debug.WriteLine("No users found.");
                        return null;
                    }

                    return productList;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error finding user list: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);

                return null;
            }
        }
    }
}
