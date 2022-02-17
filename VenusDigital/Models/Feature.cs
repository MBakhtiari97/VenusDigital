using System;
using System.Collections.Generic;

#nullable disable

namespace VenusDigital.Models
{
    public partial class Feature
    {
        public int FeatureId { get; set; }
        public int ProductId { get; set; }
        public string FeatureTitle { get; set; }
        public string FeatureValue { get; set; }

        public virtual Product Product { get; set; }
    }
}
