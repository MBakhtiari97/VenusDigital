using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VenusDigital.Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(150)]
        public string CategoryName { get; set; }
        [Required]
        public int ParentId { get; set; }
        [Required]
        [MaxLength(150)]
        public string ParentName { get; set; }
        [Required]
        [MaxLength(150)]
        public string ParentCategoryBanner { get; set; }
        [Required]
        public int ProductId { get; set; }

        //Nav
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
        public Categories Category { get; set; }

    }
}
