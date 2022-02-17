using System;
using System.Collections.Generic;

#nullable disable

namespace VenusDigital.Models
{
    public partial class PostalInformation
    {
        public int PostalInformationId { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string TelephoneNumber { get; set; }

        public virtual User User { get; set; }
    }
}
