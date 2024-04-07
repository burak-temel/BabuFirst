using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasOne(p => p.TaxRate)
                   .WithMany()
                   .HasForeignKey(p => p.TaxRateId);
            builder.HasMany(p => p.ServiceItems)
                   .WithOne(si => si.Product)
                   .HasForeignKey(si => si.ProductId);
            builder.HasOne(p => p.Organization)
                   .WithMany()
                   .HasForeignKey(p => p.OrganizationId);
        }
    }
}