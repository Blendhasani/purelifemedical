using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PurelifeMedical.Models
{
    public class Kujdestarite
    {
        public int Id { get; set; }


        [Required]
        public DateTime Data { get; set; }
        [Required]
        public int Kati { get; set; }
        [Required]
        public string Reparti { get; set; }

        public string InsertedFrom { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedFrom { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("StafiId")]
        public int StafiId { get; set; }
        public Stafi Stafi { get; set; }
    }
}
