using System.IdentityModel.Tokens.Jwt;

namespace Common.Helper
{

    public class TokenHelper
    {
        public static string? GetUserGuidFromToken(string userToken)
        {
            var token = userToken["Bearer ".Length..].Trim();
            var handler = new JwtSecurityTokenHandler();

            if (handler.ReadToken(token) is not JwtSecurityToken jwtToken)
            {
                Console.Error.WriteLine("Invalid token.");
                return null;
            }

            var userGuidClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name");
            if (userGuidClaim == null)
            {
                Console.Error.WriteLine("User ID not found in token.");
                return null;
            }

            return userGuidClaim.Value;
        }
    }



}