using System.ComponentModel.DataAnnotations;

namespace VenusDigital.Models
{
    public class Newsletters
    {
        [Key]
        public int NewsletterId { get; set; }
        [Required]
        [MaxLength(250)]
        public string NewslettersSubedUserEmail { get; set; }
    }
}
