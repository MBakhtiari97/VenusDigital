using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;
using Microsoft.AspNetCore.Http;

namespace VenusDigital.Models
{
    public class ProductGalleries
    {
        [Key]
        public int GalleryId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Display(Name = "Image")]
        public string ImageName { get; set; }
        [MaxLength(250)]
        [Display(Name = "Refer To")]
        public string ImageRefersTo { get; set; }

        [Required]
        [MaxLength(250)]
        [Display(Name = "Image Alternate Text")]
        public string ImageAltName { get; set; }


        //NAV
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
    }
}
