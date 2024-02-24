using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class VehicleEntityConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(v => v.LicensePlate).IsRequired().HasMaxLength(20);
            builder.Property(v => v.Make).IsRequired().HasMaxLength(50);
            builder.Property(v => v.Model).IsRequired().HasMaxLength(50);
            builder.Property(v => v.Year).IsRequired();
            builder.Property(v => v.VIN).IsRequired().HasMaxLength(17);
            builder.Property(v => v.Mileage).IsRequired();

            builder.HasOne(v => v.Customer)
                   .WithMany(c => c.Vehicles)
                   .HasForeignKey(v => v.CustomerId);
        }
    }

}