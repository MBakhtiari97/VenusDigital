using System;
using System.Collections.Generic;

#nullable disable

namespace VenusDigital.Models
{
    public partial class ContactU
    {
        public int ContactId { get; set; }
        public string UserFullName { get; set; }
        public string UserEmailAddress { get; set; }
        public string RequestTitle { get; set; }
        public string RequestDescription { get; set; }
        public string RequestCode { get; set; }
    }
}
