using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> modelBuilder)
        {
            modelBuilder.HasData(
               new Hotel
               {
                   Id = 1,
                   Name = "Sandals Resort and Spa",
                   Address = "Negril",
                   CoutryId = 1,
                   Rating = 4.5
               },
               new Hotel
               {
                   Id = 2,
                   Name = "Grand Palidium",
                   Address = "Nassua",
                   CoutryId = 2,
                   Rating = 4.5
               },
               new Hotel
               {
                   Id = 3,
                   Name = "comfort Suites",
                   Address = "Goerge Town",
                   CoutryId = 3,
                   Rating = 4.3
               });

        }
    }
}
