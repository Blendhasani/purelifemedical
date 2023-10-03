using Microsoft.AspNetCore.Identity;
using PurelifeMedical.Models;
using System.Security.Claims;

namespace PurelifeMedical.Services
{
	public class CurrentUser : ICurrentUser
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CurrentUser(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}

		public string GetCurrentUserName()
		{
			var email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
			var user = _userManager.FindByEmailAsync(email).Result;
			return user?.FullName;
		}

	}
}
