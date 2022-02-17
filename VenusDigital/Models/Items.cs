using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VenusDigital.Models
{
    public class Items
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        public int ItemCount { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public decimal ItemTotalPrice { get; set; }

        //Nav
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
        public Cart Cart { get; set; }
    }
}
