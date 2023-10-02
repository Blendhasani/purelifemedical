using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PurelifeMedical.Models
{
    public class Stafi
    {
        public int Id { get; set; }
        public string? NrLeternjoftimit { get; set; }
        [Required]
        public string Emri { get; set; }
        [Required]
        public string Mbiemri { get; set; }
        public string? Gjinia { get; set; }
        public DateTime? DataLindjes { get; set; }
        public string? NrLincences { get; set; }
        public string? NrTel { get; set; }
        public string? PictureUrl { get; set; }
        public string PublicId { get; set; }
        public int RoletId { get; set; }
        [ForeignKey("RoletId")]
        public int? ShtetiId { get; set; }
        [ForeignKey("ShtetiId")]

        public string? Qyteti { get; set; }
        public int? NacionalitetiId { get; set; }
        [ForeignKey("NacionalitetiId")]
        public int? LemiaId { get; set; }
        [ForeignKey("LemiaId")]

        [Required]

        public string Email { get; set; }
        public string EmailZyrtar { get; set; }
        /*[Required]*/
        [DataType(DataType.Password)]
        //[StringLength(100, MinimumLength = 6, ErrorMessage = "The password must be at least {2} characters long.")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        //[Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public string InsertedFrom { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedFrom { get; set; }
        public bool IsDeleted { get; set; }

        public Lemia Lemia { get; set; }
        public Nacionaliteti Nacionaliteti { get; set; }
        public Shteti Shteti { get; set; }
        public Rolet Rolet { get; set; }
    }
}
