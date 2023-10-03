using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PurelifeMedical.Data;
using PurelifeMedical.Models;
using PurelifeMedical.ViewModels;

namespace PurelifeMedical.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ApplicationDbContext _context;
		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Login()
		{
			if (!User.Identity.IsAuthenticated)
			{

				return View(new LoginViewModel());
			}
			return RedirectToAction("Index", "Admin");

		}


		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVM)
		{
			if (!ModelState.IsValid) return View(loginVM);

			var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
			if (user != null)
			{
				var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

				if (passwordCheck)
				{
					var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
					if (result.Succeeded)
					{

						var role = await _userManager.GetRolesAsync(user);
						if (role.Contains("ADMIN"))
						{
							return RedirectToAction("Index", "Admin");
						}
						else
						{
							return RedirectToAction("Index", "Home");
						}



					}
				}
				ModelState.AddModelError(string.Empty, "Invalid email or password.");
				return View(loginVM);
			}


			ModelState.AddModelError(string.Empty, "Invalid email or password.");
			return View(loginVM);
		}
		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Login", "Account");
		}
	}
}
