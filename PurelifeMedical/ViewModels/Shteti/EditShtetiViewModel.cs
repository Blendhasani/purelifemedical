namespace PurelifeMedical.ViewModels.Shteti
{
	public class EditShtetiViewModel
	{
		public int Id { get; set; }
		public string Emri { get; set; }

		public DateTime? ModifiedDate { get; set; }
		public string? ModifiedFrom { get; set; }
	}
}
