using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace Common.Helper
{
    public class ClaimHelper
    { 
        public static readonly string Role = "role";
        public static string ClaimUid = "Uid";
        public static string ClaimType_OldVersion = "name";
        public static string ClaimType_NewVersion = "unique_name";

        public static string GetClaim(string token )
        {
            if (string.IsNullOrEmpty(token)) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var claimData = securityToken.Claims
                .Where(claim => claim.Type == ClaimType_OldVersion || claim.Type == ClaimType_NewVersion)
                .FirstOrDefault();
            var stringClaimValue = claimData != null ? claimData.Value : null;
            return stringClaimValue;
        }
    }
}
