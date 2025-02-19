using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Helper
{
    public class IPHelper
    {
        public string IpAddress { get; set; }
        public IPHelper(HttpRequest request, IHttpContextAccessor accessor)
        {  
            string ip = String.Empty;
            if (request.Headers["X-Forwarded-For"].FirstOrDefault() == null)
            {
                ip = accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            }
            else
            {
                ip = request.Headers["X-Forwarded-For"].FirstOrDefault();
            }

            IpAddress = ip;
        }
    }
}
