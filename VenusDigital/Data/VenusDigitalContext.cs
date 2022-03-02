
using Microsoft.EntityFrameworkCore;
using VenusDigital.Models;

namespace VenusDigital.Data
{
    public class VenusDigitalContext:DbContext
    {
        public VenusDigitalContext(DbContextOptions<VenusDigitalContext> options):base(options)
        {
            
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Coupons> Coupons { get; set; }
        public DbSet<Features> Features { get; set; }
        public DbSet<Newsletters> Newsletters { get; set; }
        public DbSet<PostalInformations> PostalInformations { get; set; }
        public DbSet<ProductGalleries> ProductGalleries { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Supports> Supports { get; set; }
        public DbSet<WishLists> WishLists { get; set; }
        public DbSet<SelectedCategory> SelectedCategory { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Compare> Compare { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
