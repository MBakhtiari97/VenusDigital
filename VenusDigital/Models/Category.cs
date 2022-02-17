using System;
using System.Collections.Generic;

#nullable disable

namespace VenusDigital.Models
{
    public partial class Category
    {
        public Category()
        {
            InverseParent = new HashSet<Category>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ParentId { get; set; }
        public string ParentName { get; set; }
        public string ParentCategoryBaner { get; set; }
        public int ProductId { get; set; }

        public virtual Category Parent { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<Category> InverseParent { get; set; }
    }
}
