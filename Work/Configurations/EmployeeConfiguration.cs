using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Work.Data.Entites;

namespace Work.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).IsRequired(true).HasMaxLength(15);
            builder.Property(e => e.Surname).IsRequired(true).HasMaxLength(15);
            builder.Property(e => e.CreateDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(e => e.BirthDate).IsRequired(true).HasDefaultValueSql("GETUTCDATE()");

        }
    }
}
