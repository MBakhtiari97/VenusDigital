using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace VenusDigital.Areas.Admin.Models
{
    public class GalleriesViewModel
    {
        public int GalleryId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ImageName { get; set; }
        [MaxLength(250)]
        public string ImageRefersTo { get; set; }
        [Required]
        [MaxLength(250)]
        public string ImageAltName { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
