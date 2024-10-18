using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Data
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> _user)
        {
            if(_user.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    DisplayName = "Mahmoud Ibrahim",
                    Email = "mi4654110@gmail.com",
                    UserName = "Mahmoud.ibrahim",
                    PhoneNumber = "01018535996"
                };
                await _user.CreateAsync(user,"MM##mm11");
            }
        }
    }
}
