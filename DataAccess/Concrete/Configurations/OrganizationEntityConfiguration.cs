using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class OrganizationEntityConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name).HasMaxLength(100).IsRequired();
            builder.Property(o => o.Address).HasMaxLength(200).IsRequired();
        }
    }
}