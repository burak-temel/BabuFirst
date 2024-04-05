using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(c => c.LastName).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(100);
            builder.Property(c => c.PhoneNumber).HasMaxLength(15);

            builder.HasOne(c => c.Organization)
                   .WithMany()
                   .HasForeignKey(c => c.OrganizationId);
            builder.HasMany(c => c.Vehicles)
                   .WithOne(v => v.Customer)
                   .HasForeignKey(v => v.CustomerId);
        }
    }
}