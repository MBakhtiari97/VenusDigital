using System;

namespace VenusDigital.Models.ViewModels
{
    public class SingleReviewViewModel
    {
        public int ReviewId { get; set; }
        public string Username { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewDescription { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
