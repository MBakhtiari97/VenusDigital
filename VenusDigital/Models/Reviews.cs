using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VenusDigital.Models
{
    public class Reviews
    {
        [Key]
        public int ReviewId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "Title")]
        public string ReviewTitle { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(800)]
        [Display(Name = "Description")]
        public string ReviewDescription { get; set; }
        [Required]
        [Display(Name = "Score")]
        public float ReviewScore { get; set; }
        [Required]
        [Display(Name = "Create Date")]
        public DateTime ReviewCreateDate { get; set; }
        [Display(Name = "Publish Status")]
        public bool IsPublished { get; set; }

        //Nav
        [ForeignKey("UserId")]
        public Users Users { get; set; }
        [ForeignKey("ProductId")]
        public Products Products { get; set; }




    }
}
