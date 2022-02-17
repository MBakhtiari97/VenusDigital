using System;
using System.Collections.Generic;

#nullable disable

namespace VenusDigital.Models
{
    public partial class ProductGallery
    {
        public int GalleryId { get; set; }
        public int ProductId { get; set; }
        public string ImageName { get; set; }
        public string ImageRefersTo { get; set; }
        public string ImageAltName { get; set; }

        public virtual Product Product { get; set; }
    }
}
