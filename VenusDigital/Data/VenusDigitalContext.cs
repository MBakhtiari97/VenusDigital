
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
    }
}
