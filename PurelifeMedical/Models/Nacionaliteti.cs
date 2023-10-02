using System.ComponentModel.DataAnnotations;

namespace PurelifeMedical.Models
{
    public class Nacionaliteti
    {
        public int Id { get; set; }
        [Required]
        public string Emri { get; set; }
        public string InsertedFrom { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedFrom { get; set; }
        public bool IsDeleted { get; set; }
        public List<Stafi> Stafi { get; set; }
   
    }
}
