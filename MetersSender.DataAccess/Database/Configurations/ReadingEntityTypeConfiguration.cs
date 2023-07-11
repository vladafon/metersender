using MetersSender.DataAccess.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetersSender.DataAccess.Database.Configurations
{
    internal class ReadingEntityTypeConfiguration : IEntityTypeConfiguration<Reading>
    {
        public void Configure(EntityTypeBuilder<Reading> builder)
        {
            builder.HasKey(reading => reading.ReadingId);

            builder.Property(reading => reading.Value)
                .IsRequired();

            builder.HasOne(reading => reading.Request)
                .WithMany(service => service.Readings)
                .HasForeignKey(house => house.RequestId);

            builder.HasOne(reading => reading.Meter)
                .WithMany(meter => meter.Readings)
                .HasForeignKey(reading => reading.MeterId);
        }
    }
}
