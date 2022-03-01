using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace VenusDigital.Areas.Admin.Models
{
    public class CategoriesViewModel
    {
        public int CategoryId { get; set; }
        [Display(Name = "Name")]
        public string CategoryName { get; set; }
        public Nullable<int> ParentId { get; set; }
        public IFormFile ParentCategoryBanner { get; set; }
        public IFormFile CategoryBanner { get; set; }
    }
}
