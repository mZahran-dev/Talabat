using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIS.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser> FindUserAddressByEmailAsync(this UserManager<AppUser> userManager, ClaimsPrincipal userClaim)
        {
            var email = userClaim.FindFirstValue(ClaimTypes.Email);
            var user = userManager.Users.Include(u => u.Address).FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
            return user;
        }
    }
}
