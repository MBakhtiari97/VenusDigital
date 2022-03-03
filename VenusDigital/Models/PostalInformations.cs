using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace VenusDigital.Models
{
    public class PostalInformations
    {
        [Key]
        public int PostalInformationId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(800)]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Telephone Number")]
        public string TelephoneNumber { get; set; }

        //Nav
        public Users User { get; set; }

    }
}
