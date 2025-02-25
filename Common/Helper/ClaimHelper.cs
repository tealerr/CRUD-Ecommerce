using System.IdentityModel.Tokens.Jwt;

namespace Common.Helper
{
    public class ClaimHelper
    {
        public static readonly string Role = "role";
        public static readonly string ClaimUid = "Uid";
        public static readonly string ClaimType_OldVersion = "name";
        public static readonly string ClaimType_NewVersion = "unique_name";

        public static string GetClaim(string token)
        {
            if (string.IsNullOrEmpty(token)) return string.Empty;
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var claimData = securityToken?.Claims
                .Where(claim => claim.Type == ClaimType_OldVersion || claim.Type == ClaimType_NewVersion)
                .FirstOrDefault();
            var stringClaimValue = claimData != null ? claimData.Value : null;
            return stringClaimValue ?? string.Empty;
        }
    }
}
