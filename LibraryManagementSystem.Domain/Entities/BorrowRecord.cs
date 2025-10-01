namespace LibraryManagementSystem.Domain.Entities
{
    public class BorrowRecord
    {
        public int Id { get; set; }
        public DateTime BorrowedDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
