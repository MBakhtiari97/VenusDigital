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
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [MaxLength(50)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        [Required]
        public int PostalInformationId { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public int WishListId { get; set; }
        public bool IsAdmin { get; set; }
        [Display(Name = "Register Date")]
        public DateTime RegisterDate { get; set; }
        [MaxLength(50)]
        public string UserIdentifierCode { get; set; }
        public bool IsActive { get; set; }

        //Nav
        public List<PostalInformations> PostalInformations { get; set; }
        public List<WishLists> WishLists { get; set; }
        public List<Reviews> Reviews { get; set; }
        public List<Order> Orders { get; set; }
        public List<Compare> Compares { get; set; }
    }
}
