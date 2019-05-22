using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zh.Persistence;

namespace Zh.WebAPI.Controllers
{
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;

        public AccountController(SignInManager<User> signInManager)
        {
            this._signInManager = signInManager;
        }

        // api/Account/Login/username/password
        [HttpGet("login/{username}/{password}")]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (_signInManager.IsSignedIn(User))
                await _signInManager.SignOutAsync();

            if (ModelState.IsValid)
            {
                var res = await _signInManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);

                if (res.Succeeded)
                {
                    return Ok();
                }

                ModelState.AddModelError("", "Invalid username or password");
                return Unauthorized();
            }
            return Unauthorized();
        }

        // api/Account/Logout
        [HttpGet("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }
    }
}