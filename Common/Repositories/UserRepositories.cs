using System.Diagnostics;
using Common.Helper;
using Common.Models;
using Common.Request;
using SqlKata.Execution;

namespace Common.Repositories
{
    public class UserRepositories
    {
        public async Task<bool> AddUserAsync(RegisterUser user, string UserGuid)
        {
            try
            {
                var newUser = new User
                {
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Nickname = user.Nickname,
                    UserGuid = UserGuid,
                    CreatedTime = DateTime.Now
                };

                var connection = new DBConnection().Connect();
                var result = await connection.Query(Table.User)
                .InsertAsync(new
                {
                    newUser.Firstname,
                    newUser.Lastname,
                    newUser.Nickname,
                    newUser.UserGuid,
                    newUser.CreatedTime
                });

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

        public async Task<bool> UpdateUserAsync(UpdateUser user)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var userInfos = await connection.Query(Table.User)
                .Where(Column.UserGuid, user.UserGuid)
                .FirstOrDefaultAsync<User>();

                if (userInfos == null)
                {
                    return false;
                }
                // Apply update
                await connection.Query(Table.User)
                .Where(Column.UserGuid, user.UserGuid)
                .UpdateAsync(new
                {
                    Firstname = user.Firstname ?? userInfos.Firstname,
                    Lastname = user.Lastname ?? userInfos.Lastname,
                    Nickname = user.Nickname ?? userInfos.Nickname
                });

                connection.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating user: {ex.Message}");
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

        public static User? GetUserById(string userId)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var user = connection.Query(Table.User)
                    .Where(Column.Id, userId)
                    .FirstOrDefault<User>();
                connection.Connection.Close();

                if (user == null)
                {
                    Debug.WriteLine($"User with ID: {userId} not found");
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error finding user with ID: {userId}: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);

                return null;
            }
        }

        public int GetStatusAspnetuserByEmail(string email)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var result = connection.Query(Table.AspNetUsers)
                    .Select(Column.Enabled)
                    .Where(Column.Email, email)
                    .Get<int>().FirstOrDefault();
                connection.Connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return 0;
            }
        }

        public int InsertAspnetusertokens(string userId, string loginProvider, string name, string value)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var insertObject = new
                {
                    UserId = userId,
                    LoginProvider = loginProvider,
                    Name = name,
                    Value = value
                };
                var result = connection.Query(Table.AspNetUserTokens).InsertGetId<int>(insertObject);
                connection.Connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return 0;
            }
        }

        public bool ValidateToken(string token)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var result = connection.Query(Table.AspNetUserTokens)
                   .Select(Column.Value)
                   .Where(Column.Value, token)
                   .Get<string>()
                   .FirstOrDefault();
                connection.Connection.Close();
                if (string.IsNullOrEmpty(result))
                {
                    return false;
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

        public static string? GetUserToken(string token)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var result = connection.Query(Table.AspNetUserTokens)
                   .Select(Column.Value)
                   .Where(Column.Value, token)
                   .Get<string>()
                   .FirstOrDefault();
                connection.Connection.Close();
                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public static bool RemoveToken(string token)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var result = connection.Query(Table.AspNetUserTokens)
                   .Where(Column.Value, token)
                   .Delete();
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
                return false;
            }
        }
    }
}
