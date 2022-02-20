using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VenusDigital.Models
{
    public class SelectedCategory
    {
        [Key]
        public int SelectedCategoryId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int ProductId { get; set; }

        //Nav
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
        [ForeignKey("CategoryId")]
        public Categories Categories { get; set; }

    }
}
