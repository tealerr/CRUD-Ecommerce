using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class ActivityLogModel
    {
        public string display_name { get; set; }
        public string ip_address { get; set; }
        public string activity { get; set; }
        public int activity_type_id { get; set; }
        public string target { get; set; }
        public DateTime created_time { get; set; }
        public string detail { get; set; }

    }
}
