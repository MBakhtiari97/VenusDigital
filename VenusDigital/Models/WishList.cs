using System;
using System.Collections.Generic;

#nullable disable

namespace VenusDigital.Models
{
    public partial class WishList
    {
        public int WishListId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
