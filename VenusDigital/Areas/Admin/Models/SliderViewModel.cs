using Microsoft.AspNetCore.Http;

namespace VenusDigital.Areas.Admin.Models
{
    public class SliderViewModel
    {
        public int SlideId { get; set; }
        public IFormFile SlideName { get; set; }
        public string SlideAltName { get; set; }
        public string? SlideRefersTo { get; set; }
    }
}
