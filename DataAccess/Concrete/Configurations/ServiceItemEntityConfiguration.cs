using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class ServiceItemEntityConfiguration : IEntityTypeConfiguration<ServiceItem>
    {
        public void Configure(EntityTypeBuilder<ServiceItem> builder)
        {
            builder.HasKey(si => new { si.ServiceRecordId, si.ProductId });
            builder.Property(si => si.Quantity).IsRequired();
            builder.Property(si => si.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasOne(si => si.ServiceRecord)
                   .WithMany(sr => sr.ServiceItems)
                   .HasForeignKey(si => si.ServiceRecordId);
            builder.HasOne(si => si.Product)
                   .WithMany(p => p.ServiceItems)
                   .HasForeignKey(si => si.ProductId);
        }
    }
}