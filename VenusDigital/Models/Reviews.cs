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
        public string ReviewTitle { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(800)]
        public string ReviewDescription { get; set; }
        [Required]
        public float ReviewScore { get; set; }
        [Required]
        public DateTime ReviewCreateDate { get; set; }
        public bool IsPublished { get; set; }

        //Nav
        [ForeignKey("UserId")]
        public Users Users { get; set; }
        [ForeignKey("ProductId")]
        public Products Products { get; set; }




    }
}
