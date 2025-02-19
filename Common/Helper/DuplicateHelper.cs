using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Helper
{
    public class DuplicateHelper
    {

        public static bool CheckDuplicate(string table,string column,string input,string columnName = "",int id = 0)
        {
            var connection = new DBConnection().Connect();
            var query = connection.Query($"{table}").SelectRaw("count(*)").Where($"{column}", input);
            if (id != 0 && !string.IsNullOrEmpty(columnName))
            {
                query.WhereNot($"{columnName}", id);
            }
            var result = query.Get<int>().FirstOrDefault();
            connection.Connection.Close();
            return result == 0 ? false : true;
        }

        public static bool CheckDuplicateByUserGuid(string table, string column, string input,string userGuid, string columnName = "", int id = 0)
        {
            var connection = new DBConnection().Connect();
            var query = connection.Query($"{table}").SelectRaw("count(*)").Where($"{column}", input).Where(Column.UserGuid, userGuid);
            if (id != 0 && !string.IsNullOrEmpty(columnName))
            {
                query.WhereNot($"{columnName}", id);
            }
            var result = query.Get<int>().FirstOrDefault();
            connection.Connection.Close();
            return result == 0 ? false : true;
        }
    }
}