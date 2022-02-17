using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VenusDigital.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public bool IsFinally { get; set; }
        
        //Nav
        [ForeignKey("UserId")]
        public Users Users { get; set; }
        public List<Items> Items { get; set; }
    }
}
