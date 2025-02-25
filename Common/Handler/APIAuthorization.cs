using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Handler
{
    public class APIAuthorization : IAuthorizationRequirement
    {
        public IReadOnlyList<string> ApiKeys { get; set; }

        public APIAuthorization(IEnumerable<string> apiKeys)
        {
            ApiKeys = apiKeys?.ToList() ?? new List<string>();
        }
    }
}
