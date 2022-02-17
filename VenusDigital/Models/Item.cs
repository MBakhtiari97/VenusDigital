using System;
using System.Collections.Generic;

#nullable disable

namespace VenusDigital.Models
{
    public partial class Item
    {
        public Item()
        {
            Carts = new HashSet<Cart>();
        }

        public int ItemId { get; set; }
        public int ItemCount { get; set; }
        public int ProductId { get; set; }
        public decimal ItemTotallPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
