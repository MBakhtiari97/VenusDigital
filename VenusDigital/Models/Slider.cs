using System.ComponentModel.DataAnnotations;

namespace VenusDigital.Models
{
    public class Slider
    {
        [Key]
        public int SlideId { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Slide")]
        public string SlideName { get; set; }
        [Required]
        [MaxLength(200)]
        [Display(Name = "Slide")]
        public string SlideAltName { get; set; }
        [Display(Name = "Slide")]
        public string? SlideRefersTo { get; set; }
    }
}
