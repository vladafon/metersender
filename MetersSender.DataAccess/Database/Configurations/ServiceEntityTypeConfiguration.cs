using MetersSender.DataAccess.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetersSender.DataAccess.Database.Configurations
{
    internal class ServiceEntityTypeConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(service => service.Serviced);

            builder.Property(service => service.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
