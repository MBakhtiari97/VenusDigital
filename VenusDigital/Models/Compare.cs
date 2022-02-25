using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VenusDigital.Models
{
    public class Compare
    {
        [Key]
        public int CompareId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ProductId { get; set; }
        //Nav
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
        [ForeignKey("UserId")]
        public Users Users { get; set; }
    }
}
