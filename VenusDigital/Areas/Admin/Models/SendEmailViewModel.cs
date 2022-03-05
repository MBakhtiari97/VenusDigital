using System.ComponentModel.DataAnnotations;

namespace VenusDigital.Areas.Admin.Models
{
    public class SendEmailViewModel
    {
        [Display(Name = "Email Title")]
        public string Title { get; set; }
        [Display(Name = "Email Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class SendNewslettersViewModel
    {
        [Display(Name = "Email Title")]
        public string Title { get; set; }
        [Display(Name = "Email Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
