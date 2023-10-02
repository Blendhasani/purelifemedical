using System.ComponentModel.DataAnnotations;

namespace PurelifeMedical.Models
{
    public class DitetEPushimeve
    {
        public int Id { get; set; }
        [Required]
        public string Emri { get; set; }
        [Required]
        public DateTime Festa { get; set; }
        [Required]
        public DateTime DitaEPushimit { get; set; }

        public string InsertedFrom { get; set; }

        public DateTime InsertedDate { get; set; }

    }
}
