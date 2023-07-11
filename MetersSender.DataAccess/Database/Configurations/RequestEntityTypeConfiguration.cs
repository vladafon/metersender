using MetersSender.DataAccess.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetersSender.DataAccess.Database.Configurations
{
    internal class RequestEntityTypeConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasKey(request => request.RequestId);

            builder.Property(request => request.ReceivingDateTimeUtc)
                .IsRequired();
            
            builder.Property(request => request.SendingDateTimeUtc);
        }
    }
}