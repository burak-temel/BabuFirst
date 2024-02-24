using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class ServiceRecordEntityConfiguration : IEntityTypeConfiguration<ServiceRecord>
    {
        public void Configure(EntityTypeBuilder<ServiceRecord> builder)
        {
            builder.HasKey(sr => sr.Id);
            builder.Property(sr => sr.ServiceDate).IsRequired();
            builder.Property(sr => sr.Description).HasMaxLength(500);
            builder.Property(sr => sr.LaborCost).HasColumnType("decimal(18,2)");

            builder.HasOne(sr => sr.Vehicle)
                   .WithMany(v => v.ServiceRecords)
                   .HasForeignKey(sr => sr.VehicleId);

            builder.HasOne(sr => sr.Invoice)
                   .WithMany(i => i.ServiceRecords)
                   .HasForeignKey(sr => sr.InvoiceId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(sr => sr.ServiceItems)
                   .WithOne(si => si.ServiceRecord)
                   .HasForeignKey(si => si.ServiceRecordId);
        }
    }

}