using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class ActivityLogModel
    {
        public string Display_name { get; set; } = string.Empty;
        public string Ip_address { get; set; } = string.Empty;
        public string Activity { get; set; } = string.Empty;
        public int Activity_type_id { get; set; }
        public string Target { get; set; } = string.Empty;
        public DateTime Created_time { get; set; }
        public string Detail { get; set; } = string.Empty;
    }
}
