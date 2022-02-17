using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VenusDigital.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(250)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(250)]
        public string UserFullName { get; set; }
        [Required]
        [MaxLength(250)]
        public string EmailAddress { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(1024)]
        public Byte[] Password { get; set; }
        [Required]
        public int PostalInformationId { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public int WishListId { get; set; }
        public bool IsAdmin { get; set; }

        //Nav
        public List<PostalInformations> PostalInformations { get; set; }
        public List<WishLists> WishLists { get; set; }
        public List<Reviews> Reviews { get; set; }
        public List<Cart> Carts { get; set; }
    }
}
