using MetersSender.DataAccess.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetersSender.DataAccess.Database.Configurations
{
    internal class MeterEntityTypeConfiguration : IEntityTypeConfiguration<Meter>
    {
        public void Configure(EntityTypeBuilder<Meter> builder)
        {
            builder.HasKey(meter => meter.MeterId);

            builder.Property(meter => meter.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(meter => meter.SourceMeterId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(meter => meter.RecepientMeterId)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(meter => meter.House)
                .WithMany(house => house.Meters)
                .HasForeignKey(meter => meter.HouseId);
        }
    }
}
