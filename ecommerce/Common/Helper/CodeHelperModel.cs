using Common.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Helper
{
    public class CodeHelperModel
    {
        public CodeHelperModel(HttpContext httpContext)
        {
            var header = httpContext.Request.Headers[CodeHelper.AUHTORIZE_HEADER_NAME].FirstOrDefault() ?? null;
            var token = header?.Substring("Bearer ".Length);
            var userId = ClaimHelper.GetClaim(token);
            UserId = userId;
            Token = token;
            //LanguageId = int.Parse((httpContext.Items[CodeHelper.LANGUAGE_ID_KEY] as string) ?? CodeHelper.LANGUAGE_BASE_ID);
            LanguageId = int.Parse((httpContext.Request.Headers[CodeHelper.LANGUAGEID].FirstOrDefault() as string) ?? CodeHelper.LANGUAGE_BASE_ID);
            IsLogin = true;
            MemberId = new UserRepository().GetMemberId(userId);
            BranchId = new UserRepository().GetBranchIdByUserGuid(userId);
        }

        public string UserId { get; }
        public string Token { get; }
        public int LanguageId { get; set; }

        public bool IsLogin { get; }
        public int BrandId { get; set; }
        public string MemberId { get; set; }
        public int? BranchId { get; set; }
    }
}
