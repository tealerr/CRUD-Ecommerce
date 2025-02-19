using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Common.Helper
{
    public class ObjectHandler
    {
        public static double GetDouble(object o)
        {
            if (o == null)
            {
                return 0;
            }
            else
            {
                if (double.TryParse(o.ToString(), out double output))
                {
                    return output;
                }
                else
                {
                    return 0;
                }
            }
        }

        public static DateTime? GetDateTime(string o, string format)
        {
            try
            {
                var output = DateTime.ParseExact(o, format, CultureInfo.InvariantCulture);
                return output;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
