using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models.Utilities
{
    public class InternalLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ExternalAzureLogin
    {
        public int registerTypeId { get; set; }
        public string id_token { get; set; }
        public string Telephone { get; set; }
        public string SocialProvider { get; set; }
        public string SocialId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
