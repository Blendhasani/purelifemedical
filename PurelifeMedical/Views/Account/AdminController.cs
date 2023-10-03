using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PurelifeMedical.Data.Static;
using PurelifeMedical.Data;
using PurelifeMedical.Models;
using Microsoft.EntityFrameworkCore;
using PurelifeMedical.ViewModels;

namespace PurelifeMedical.Views.Account
{
	[Authorize(Roles = "ADMIN")]
	public class AdminController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;


		public AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;

		}
		public async Task<IActionResult> Index()
		{
			var stafiEmails = await _context.Stafi
				.Where(s => !s.IsDeleted)
				.Select(s => s.Email)
				.ToListAsync();
			var usersWithPermission = await _userManager.GetUsersInRoleAsync("ADMIN");
			var notstafusers = await _userManager.GetUsersInRoleAsync("USER");


			var users = await _userManager.Users
				.Where(u => stafiEmails.Contains(u.Email) || usersWithPermission.Select(x => x.Email).Contains(u.Email) || notstafusers.Select(x => x.Email).Contains(u.Email))
			.ToListAsync();

			var userRoles = new List<UsersRoles>();

			foreach (var user in users)
			{
				var roles = await _userManager.GetRolesAsync(user);
				userRoles.Add(new UsersRoles()
				{
					User = user,
					Roles = roles
				});
			}

			return View(userRoles.OrderBy(u => u.User.Email));
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(AdminUserViewModel auser)
		{


			var newUser = new ApplicationUser()
			{
				FullName = auser.Name,
				Email = auser.Email,
				UserName = auser.Email
			};

			var newUserResponse = await _userManager.CreateAsync(newUser, auser.Password);

			if (newUserResponse.Succeeded)
				await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
			await _context.SaveChangesAsync();

			//return View("~/Report/Index.cshtml");
			return RedirectToAction(nameof(Index));
			/*   return newUser;*/
		}

		[HttpPost]
		public async Task<IActionResult> DeleteUser(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				var roles = await _userManager.GetRolesAsync(user);
				foreach (var role in roles)
				{
					await _userManager.RemoveFromRoleAsync(user, role);
				}
				await _userManager.DeleteAsync(user);
			}
			return RedirectToAction(nameof(Index));
		}



	}
}
