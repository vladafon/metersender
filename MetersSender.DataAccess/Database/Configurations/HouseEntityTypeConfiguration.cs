using MetersSender.DataAccess.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetersSender.DataAccess.Database.Configurations
{
    internal class HouseEntityTypeConfiguration : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            builder.HasKey(house => house.HouseId);

            builder.Property(house => house.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(house => house.SourceService)
                .WithMany(service => service.HousesSource)
                .HasForeignKey(house => house.SourceServiceId);

            builder.HasOne(house => house.RecepientService)
                .WithMany(service => service.HousesRecipient)
                .HasForeignKey(house => house.RecepientServiceId);
        }
    }
}
