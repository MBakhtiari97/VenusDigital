using System.ComponentModel.DataAnnotations;

namespace VenusDigital.Models
{
    public class Newsletters
    {
        [Key]
        public int NewsletterId { get; set; }
        [Required]
        [MaxLength(250)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email Address")]
        public string NewslettersSubedUserEmail { get; set; }
    }
}
