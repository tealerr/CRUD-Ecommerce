using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Responses
{
    public class ActivityLogResponse
    {
        public int id { get; set; }
        public string display_name { get; set; }
        public string ip_address { get; set; }
        public string activity { get; set; }
        public string activity_type_id { get; set; }
        public string target { get; set; }
        public DateTime created_time { get; set; }
        public string created_time_string { get { return created_time.ToString("dd/MM/yyyy HH:mm"); } }
        public string detail { get; set; }
    }
    public class ActivityTypeResponse
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
