using System.Data.Common;
using System.Diagnostics;
using Common;
using Common.Helper;
using Common.Models;
using SqlKata.Execution;

namespace Common.Repositories
{
    public class UserRepositories
    {

        public bool IsHaveExitingUser(string guid)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var result = connection.Query(Table.User)
                                   .Where(Column.UserGuid, guid)
                                   .AsCount()
                                   .FirstOrDefault<int>();
                connection.Connection.Close();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return true;
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

                if (result > 0)
                {
                    return true;
                }
                throw new Exception("User not registered.");
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

                    return userList;
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
