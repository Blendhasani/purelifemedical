using Microsoft.AspNetCore.Identity;

namespace PurelifeMedical.Models
{
	public class UsersRoles
	{
		public IdentityUser User { get; set; }
		public IList<string> Roles { get; set; }
	}
}
