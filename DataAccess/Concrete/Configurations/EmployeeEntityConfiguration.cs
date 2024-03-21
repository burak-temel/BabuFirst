using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class EmployeeEntityConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(e => e.LastName).HasMaxLength(50).IsRequired();
            builder.Property(e => e.Email).HasMaxLength(100).IsRequired();
            builder.Property(e => e.PhoneNumber).HasMaxLength(15).IsRequired();
            builder.Property(e => e.Salary).HasColumnType("decimal(18,2)");

            builder.HasOne(e => e.Organization)
                   .WithMany(o => o.Employees)
                   .HasForeignKey(e => e.OrganizationId);
        }
    }
}