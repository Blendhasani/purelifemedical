using System.ComponentModel.DataAnnotations;

namespace PurelifeMedical.ViewModels
{
	public class AdminUserViewModel
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
