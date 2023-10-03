namespace PurelifeMedical.ViewModels.Rolet
{
    public class EditRoliViewModel
    {
        public int Id { get; set; }
        public string Emri { get; set; }

        public string? ModifiedFrom { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
