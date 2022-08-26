using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Work.Data.Entites;

namespace Work.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department> 
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.CreateDate).HasDefaultValueSql("GETUTCDATE()");
        }
    }
    
}
