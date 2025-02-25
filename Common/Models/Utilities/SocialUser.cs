using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models.Utilities
{
    public class SocialUser
    {
        public string SocialProvider { get; set; }
        public string SocialId { get; set; }
        public string UserGuid { get; set; }
    }
}
