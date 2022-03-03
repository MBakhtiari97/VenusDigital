using System;
using System.ComponentModel.DataAnnotations;

namespace VenusDigital.Models
{
    public class Supports
    {
        [Key]
        public int ContactId { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "Name")]
        public string UserFullName { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "Email Address")]
        public string UserEmailAddress { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "Title")]
        public string RequestTitle { get; set; }
        [Required]
        [MaxLength(1500)]
        [Display(Name = "Description")]
        public string RequestDescription { get; set; }
        [Required]
        [MaxLength(50)]
        public string RequestCode { get; set; }
        [Required]
        [Display(Name = "Answer Status")]
        public bool IsAnswered { get; set; }
        [Display(Name = "Answer Description")]
        public string? AnswerDescription { get; set; }
        [Display(Name = "Answer Date")]
        public DateTime? AnswerDate { get; set; }
    }
}
