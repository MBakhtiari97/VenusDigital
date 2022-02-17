using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VenusDigital.Models
{
    public class Tags
    {
        [Key]
        public int TagId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(250)]
        public string Tag { get; set; }


        //Nav
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
    }
}
