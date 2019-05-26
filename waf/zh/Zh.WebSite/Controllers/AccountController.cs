using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zh.Persistence;
using Zh.WebSite.Models;

namespace Zh.WebSite.Controllers
{
	public class AccountController : BaseController
    {
		// private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		
		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
		    : base()
		{
			// _userManager = userManager;
			_signInManager = signInManager;
		}

		public IActionResult Index()
        {
			return RedirectToAction("Login");
        }

        /// <summary>
        /// Bejelentkezés.
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
			return View("Login");
        }

        /// <summary>
        /// Bejelentkezés.
        /// </summary>
        /// <param name="user">A bejelentkezés adatai.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
			if (!ModelState.IsValid)
                return View("Login", user);
            
	        var result = await _signInManager.PasswordSignInAsync(user.UserName, user.UserPassword, false, false);
			if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Hibás felhasználónév, vagy jelszó.");
                return View("Login", user);
            }

			return RedirectToAction("Index", "Home"); // átirányítjuk a főoldalra
        }
        
        /*
		/// <summary>
		/// Regisztráció.
		/// </summary>
		[HttpGet]
		public IActionResult Register()
		{
			return View("Register");
		}
        
		/// <summary>
		/// Regisztráció.
		/// </summary>
		/// <param name="user">Regisztrációs adatok.</param>
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationViewModel user)
        {
			// végrehajtjuk az ellenőrzéseket
			if (!ModelState.IsValid)
                return View("Register", user);

	        Guest guest = new Guest
	        {
				UserName = user.UserName,
				Email = user.GuestEmail,
				Name = user.GuestName,
				Address = user.GuestAddress,
				PhoneNumber = user.GuestPhoneNumber
	        };
	        var result = await _userManager.CreateAsync(guest, user.UserPassword);
	        if (!result.Succeeded)
	        {
		        // Felvesszük a felhasználó létrehozásával kapcsolatos hibákat.
				foreach (var error in result.Errors)
			        ModelState.AddModelError("", error.Description);
                return View("Register", user);
            }

	        await _signInManager.SignInAsync(guest, false); // be is jelentkeztetjük egyből a felhasználót
	        _applicationState.UserCount++; // módosítjuk a felhasználók számát
			return RedirectToAction("Index", "Home"); // átirányítjuk a főoldalra
		}
        */
        
        /// <summary>
        /// Kijelentkezés.
        /// </summary>
        public async Task<IActionResult> Logout()
        {
	        await _signInManager.SignOutAsync();

			return RedirectToAction("Index", "Home");
        }
    }
}