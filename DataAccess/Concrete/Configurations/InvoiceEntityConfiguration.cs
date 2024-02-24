using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class InvoiceEntityConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.InvoiceDate).IsRequired();
            builder.Property(i => i.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();
            // Add other properties as necessary

            builder.HasOne(i => i.Customer)
                   .WithMany(c => c.Invoices)
                   .HasForeignKey(i => i.CustomerId);
            builder.HasMany(i => i.ServiceRecords)
                   .WithOne(sr => sr.Invoice)
                   .HasForeignKey(sr => sr.InvoiceId);
        }
    }
}