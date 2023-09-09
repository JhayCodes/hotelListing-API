using HotelListing.Configurations.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApiUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {      
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //function to seed country data
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            //function to seed hotel data
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
            //function to seed role data
            modelBuilder.ApplyConfiguration(new RoleConfiguration()); 

            
        }

    }
}
