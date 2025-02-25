using Common.Helper;
using Common.Models;
using Common.Request;
using Common.Responses;
using SqlKata.Execution;
using System.Diagnostics;

namespace Common.Repositories
{
    public class ActivityLogRepository
    {

        public List<ActivityLogResponse> GetActivityLogList(ActivityLogRequest input)
        {
            try
            {
                var skip = input.page * input.take - input.take;
                var connection = new DBConnection().Connect();
                var raw_query = connection.Query(Table.ActivityLog)
                                .Join(Table.ActivityType, $"{Table.ActivityLog}.{Column.ActivityTypeId}", $"{Table.ActivityType}.{Column.Id}")
                                .SelectRaw($"{Table.ActivityLog}.*")
                                .SelectRaw($"{Table.ActivityType}.{Column.Name} AS activity");
                #region Filter
                if (!string.IsNullOrEmpty(input.search))
                {
                    raw_query.Where(w => w.WhereRaw($"{Table.ActivityLog}.display_name LIKE CONCAT('%{input.search}%')")
                                    .OrWhereRaw($"{Table.ActivityLog}.{Column.IpAddress} LIKE CONCAT('%{input.search}%')"));
                }

                input.start_date = string.IsNullOrEmpty(input.start_date) ? "2000-01-01" : input.start_date;
                input.end_date = string.IsNullOrEmpty(input.end_date) ? DateTime.Now.ToString("yyyy-MM-dd") : input.end_date;
                raw_query.WhereRaw($"date({Table.ActivityLog}.{Column.CreatedTime}) between '{input.start_date}' and '{input.end_date}'");

                if (input.activity_type != null && input.activity_type.Count() > 0)
                {
                    raw_query.WhereIn($"{Table.ActivityLog}.{Column.ActivityTypeId}", input.activity_type);
                }
                #endregion
                var result = raw_query.Limit(input.take)
                           .Offset(skip)
                           .OrderByDesc($"{Table.ActivityLog}.{Column.Id}")
                           .Get<ActivityLogResponse>()
                           .ToList();
                connection.Connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return new List<ActivityLogResponse>();
            }
        }
        public int CountActivityLogList(ActivityLogRequest input)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var raw_query = connection.Query(Table.ActivityLog)
                    .Join(Table.ActivityType, $"{Table.ActivityLog}.{Column.ActivityTypeId}", $"{Table.ActivityType}.{Column.Id}");
                #region Filter
                if (!string.IsNullOrEmpty(input.search))
                {
                    raw_query.Where(w => w.WhereRaw($"{Table.ActivityLog}.display_name LIKE CONCAT('%{input.search}%')")
                                    .OrWhereRaw($"{Table.ActivityLog}.{Column.IpAddress} LIKE CONCAT('%{input.search}%')"));
                }

                input.start_date = string.IsNullOrEmpty(input.start_date) ? "2000-01-01" : input.start_date;
                input.end_date = string.IsNullOrEmpty(input.end_date) ? DateTime.Now.ToString("yyyy-MM-dd") : input.end_date;
                raw_query.WhereRaw($"date({Table.ActivityLog}.{Column.CreatedTime}) between '{input.start_date}' and '{input.end_date}'");

                if (input.activity_type != null && input.activity_type.Count() > 0)
                {
                    raw_query.WhereIn($"{Table.ActivityLog}.{Column.ActivityTypeId}", input.activity_type);
                }
                #endregion
                var total_count = raw_query.AsCount()
                                .First<int>();
                connection.Connection.Close();
                return total_count;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return 0;
            }
        }

        public BaseResponse GetActivityLogDetail(int id)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var log = connection.Query(Table.ActivityLog)
                               .Where(Column.Id, id)
                               .FirstOrDefault<ActivityLogResponse>();
                connection.Connection.Close();
                return new BaseResponse().Success(log);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return new BaseResponse().Fail(new object());
            }
        }

        public BaseResponse ActivityTypeList()
        {
            try
            {
                var connection = new DBConnection().Connect();
                var list = connection.Query(Table.ActivityType)
                               .Get<ActivityTypeResponse>()
                               .ToList();
                connection.Connection.Close();
                return new BaseResponse().Success(list);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return new BaseResponse().Fail(new object());
            }
        }

        public List<ActivityLogResponse> GetActivityLogListExport(ActivityLogRequest input)
        {
            try
            {
                var connection = new DBConnection().Connect();
                var raw_query = connection.Query(Table.ActivityLog)
                                .Join(Table.ActivityType, $"{Table.ActivityLog}.{Column.ActivityTypeId}", $"{Table.ActivityType}.{Column.Id}")
                                .SelectRaw($"{Table.ActivityLog}.*")
                                .SelectRaw($"{Table.ActivityType}.{Column.Name} AS activity");
                #region Filter
                if (!string.IsNullOrEmpty(input.search))
                {
                    raw_query.Where(w => w.WhereRaw($"{Table.ActivityLog}.display_name LIKE CONCAT('%{input.search}%')")
                                    .OrWhereRaw($"{Table.ActivityLog}.{Column.IpAddress} LIKE CONCAT('%{input.search}%')"));
                }

                input.start_date = string.IsNullOrEmpty(input.start_date) ? "2000-01-01" : input.start_date;
                input.end_date = string.IsNullOrEmpty(input.end_date) ? DateTime.Now.ToString("yyyy-MM-dd") : input.end_date;
                raw_query.WhereRaw($"date({Table.ActivityLog}.{Column.CreatedTime}) between '{input.start_date}' and '{input.end_date}'");

                if (input.activity_type != null && input.activity_type.Count() > 0)
                {
                    raw_query.WhereIn($"{Table.ActivityLog}.{Column.ActivityTypeId}", input.activity_type);
                }
                #endregion
                var result = raw_query
                           .Get<ActivityLogResponse>()
                           .ToList();
                connection.Connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);

                return new List<ActivityLogResponse>();
            }
        }

        public BaseResponse ValidateParam(ActivityLogRequest input)
        {
            input.start_date = string.IsNullOrEmpty(input.start_date) ? "2000-01-01" : input.start_date;
            input.end_date = string.IsNullOrEmpty(input.end_date) ? DateTime.Now.ToString("yyyy-MM-dd") : input.end_date;
            DateTime start = Convert.ToDateTime(input.start_date);
            DateTime end = Convert.ToDateTime(input.end_date);
            if (start > end)
            {
                return new BaseResponse().Fail(null, "วันที่เริ่มต้นต้องน้อยกว่าวันที่สิ้นสุด");
            }

            return new BaseResponse().Success(new object());
        }

    }
}
