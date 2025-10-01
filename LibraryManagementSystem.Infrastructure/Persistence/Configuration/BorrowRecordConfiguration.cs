using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configuration
{
    public class BorrowRecordConfiguration : IEntityTypeConfiguration<BorrowRecord>
    {
        public void Configure(EntityTypeBuilder<BorrowRecord> builder)
        {
            builder.ToTable("BorrowRecords");

            builder.HasKey(br => br.Id);

            builder.Property(br => br.BorrowedDate)
                .IsRequired();

            builder.Property(br => br.DueDate)
                .IsRequired();

            builder.Property(br => br.ReturnedDate)
                .IsRequired(false);

            builder.Property(br => br.UserId)
                .IsRequired();

            builder.HasOne(br => br.User)
                .WithMany(x => x.BorrowRecords)
                .HasForeignKey(br => br.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(br => br.Book)
               .WithMany(b => b.BorrowRecords)
               .HasForeignKey(br => br.BookId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
