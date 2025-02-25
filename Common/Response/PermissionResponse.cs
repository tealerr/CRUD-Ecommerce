using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Responses
{
    public class PermissionResponse
    {
        public List<PermissionDetail> list { get; set; }
        public class PermissionDetail
        {
            public required string name { get; set; }
            public required string key { get; set; }
            public int display { get; set; }
            public int is_sub_menu { get; set; }
            public string key_valid
            {
                get
                {
                    if (is_sub_menu == 1)
                    {
                        return $"sub_{key}";
                    }
                    else
                    {
                        return key;
                    }
                }
                set { }
            }
        }
    }
}
