using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VenusDigital.Models
{
    public class WishLists
    {
        [Key]
        public int WishListId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ProductId { get; set; }

        //Nav
        [ForeignKey("UserId")]
        public Users Users { get; set; }
    }
}
