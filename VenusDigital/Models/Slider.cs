using System.ComponentModel.DataAnnotations;

namespace VenusDigital.Models
{
    public class Slider
    {
        [Key]
        public int SlideId { get; set; }
        [MaxLength(200)]
        [Display(Name = "Slide Name")]
        public string SlideName { get; set; }
        [Required]
        [MaxLength(200)]
        [Display(Name = "Alt Name")]
        public string SlideAltName { get; set; }
        [Display(Name = "Refers To")]
        public string? SlideRefersTo { get; set; }
    }
}
