using Microsoft.AspNetCore.Authorization;

namespace Common.Handler
{
    public class LoginAuthorization : IAuthorizationRequirement
    {
        public IReadOnlyList<string> ApiKeys { get; set; }

        public LoginAuthorization(IEnumerable<string> apiKeys)
        {
            ApiKeys = apiKeys?.ToList() ?? new List<string>();
        }
    }
}
