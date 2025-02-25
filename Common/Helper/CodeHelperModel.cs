using Microsoft.AspNetCore.Http;

namespace Common.Helper
{
    public class CodeHelperModel
    {
        public CodeHelperModel(HttpContext httpContext)
        {
            var header = httpContext.Request.Headers[CodeHelper.AUHTORIZE_HEADER_NAME].FirstOrDefault() ?? null;
            var token = header?.Substring("Bearer ".Length);
            var userId = token != null ? ClaimHelper.GetClaim(token) : null;
            UserId = userId ?? string.Empty;
            Token = token ?? string.Empty;
            LanguageId = int.Parse((httpContext.Request.Headers[CodeHelper.LANGUAGEID].FirstOrDefault() as string) ?? CodeHelper.LANGUAGE_BASE_ID);
            IsLogin = true;
        }

        public string UserId { get; }
        public string Token { get; }
        public int LanguageId { get; set; }

        public bool IsLogin { get; }
    }
}
