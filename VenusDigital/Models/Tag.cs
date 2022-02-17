using System;
using System.Collections.Generic;

#nullable disable

namespace VenusDigital.Models
{
    public partial class Tag
    {
        public int TagId { get; set; }
        public int ProductId { get; set; }
        public string Tag1 { get; set; }

        public virtual Product Product { get; set; }
    }
}
