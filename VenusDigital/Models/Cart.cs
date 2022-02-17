using System;
using System.Collections.Generic;

#nullable disable

namespace VenusDigital.Models
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public decimal TotallPrice { get; set; }
        public bool IsFinally { get; set; }

        public virtual Item Item { get; set; }
        public virtual User User { get; set; }
    }
}
