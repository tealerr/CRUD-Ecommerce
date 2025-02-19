using System;
using System.Globalization;

namespace Common.Helper
{
    public class DateTimeHelper
    {
        public static string FormatDate(string dateTime, bool isStartDate = true)
        {
            DateTime outDate;
            string convertDate = "";
            if(DateTime.TryParseExact(dateTime, "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None,  out outDate))
            {
                convertDate = outDate.ToString("yyyy-MM-dd");
            }
            else
            {
                if (isStartDate)
                {
                    convertDate = "1970-01-01";
                }
                else
                {
                    convertDate = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }

            return convertDate;
        }
    }
}