using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIS.Controllers
{
    public class AccountController : APIBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        //public async Task<ActionResult<UserDto>> Login(LoginDto)
        //{

        //}



    }
}
