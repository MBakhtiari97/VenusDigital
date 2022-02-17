using System.ComponentModel.DataAnnotations;

namespace VenusDigital.Models
{
    public class Supports
    {
        [Key]
        public int ContactId { get; set; }
        [Required]
        [MaxLength(250)]
        public string UserFullName { get; set; }
        [Required]
        [MaxLength(250)]
        public string UserEmailAddress { get; set; }
        [Required]
        [MaxLength(250)]
        public string RequestTitle { get; set; }
        [Required]
        [MaxLength(1500)]
        public string RequestDescription { get; set; }
        [Required]
        [MaxLength(50)]
        public string RequestCode { get; set; }
    }
}
