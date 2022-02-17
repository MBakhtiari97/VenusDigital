using System;
using System.Collections.Generic;

#nullable disable

namespace VenusDigital.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public string ReviewTitle { get; set; }
        public int UserId { get; set; }
        public string ReviewDescription { get; set; }
        public int ReviewScore { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
