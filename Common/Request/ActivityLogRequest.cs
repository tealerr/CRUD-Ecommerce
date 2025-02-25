using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Request
{
    public class ActivityLogRequest
    {
        public List<int> activity_type { get; set; }
        public string search { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public int page { get; set; }
        public int take { get; set; }
    }

}
