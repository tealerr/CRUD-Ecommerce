using Microsoft.AspNetCore.Identity;

namespace Common.Helper
{
    public class RoleHelper(UserManager<IdentityUser> userManager)
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        public const string Admin = "Admin";
        public const string User = "User";
        public const string Branch = "Branch";
        public const string Salesperson = "Salesperson";

        public async Task<bool> IsAdmin(string userGuid)
        {
            var user = await _userManager.FindByIdAsync(userGuid);
            if (user == null)
            {
                Console.Error.WriteLine("User not found.");
                return false;
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains(Admin))
            {
                Console.Error.WriteLine("User is not authorized to access this resource.");
                return false;
            }

            return true;
        }
    }
}
