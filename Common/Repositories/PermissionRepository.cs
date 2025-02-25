using Common.Helper;
using Common.Responses;
using SqlKata.Execution;
using System.Diagnostics;

namespace Common.Repositories
{
    public class PermissionRepository
    {
        public PermissionResponse GetPermissionList(string user_guid)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var result = new PermissionResponse();
                result.list = connection.Query(Table.AdminSection)
                            .Join(Table.Section, $"{Table.AdminSection}.{Column.SectionId}", $"{Table.Section}.{Column.Id}")
                            .Select($"{Table.Section}.{Column.Name}", $"{Table.Section}.{Column.Key}",
                            $"{Table.Section}.{Column.IsSubMenu}", $"{Table.Section}.{Column.Display}")
                            .Where($"{Table.AdminSection}.{Column.UserGuid}", user_guid)
                            .Get<PermissionResponse.PermissionDetail>().ToList();
                connection.Connection.Close();
                return result;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return new PermissionResponse();
            }
        }


    }
}
