using System;
using System.Collections.Generic;

#nullable disable

namespace VenusDigital.Models
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
            PostalInformations = new HashSet<PostalInformation>();
            Reviews = new HashSet<Review>();
            WishLists = new HashSet<WishList>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int PostalInformationId { get; set; }
        public int CartId { get; set; }
        public int WishListId { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<PostalInformation> PostalInformations { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
