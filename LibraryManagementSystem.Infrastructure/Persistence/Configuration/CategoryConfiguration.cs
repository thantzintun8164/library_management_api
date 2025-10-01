using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            {
                builder.ToTable("Categories");

                builder.HasKey(a => a.Id);

                builder.Property(x => x.Name)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(100)
                    .IsRequired();

                builder.Property(x => x.Description)
                   .HasColumnType("nvarchar")
                    .HasMaxLength(2000)
                    .IsRequired(false);
            }
        }
    }
}
