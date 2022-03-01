using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace VenusDigital.Areas.Admin.Models
{
    public class GalleriesViewModel
    {
        public int GalleryId { get; set; }
        public int ProductId { get; set; }
        public IFormFile ImageName { get; set; }
        public string ImageRefersTo { get; set; }
        public string ImageAltName { get; set; }
    }
}
