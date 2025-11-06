using EmployeeDirectory.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeDirectory.Persistence.EntityTypeConfiguration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.ID);

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Gender)
                .IsRequired();

            builder.Property(e => e.DateOfBirth)
                .IsRequired();

            builder.HasIndex(e => new { e.FullName, e.DateOfBirth });

            builder.HasIndex(e => new { e.Gender, e.FullName })
                   .HasDatabaseName("idx_gender_fullname");
        }
    }
}