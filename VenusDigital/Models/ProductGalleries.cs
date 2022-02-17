using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace VenusDigital.Models
{
    public class ProductGalleries
    {
        [Key]
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

        //NAV
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
    }
}
