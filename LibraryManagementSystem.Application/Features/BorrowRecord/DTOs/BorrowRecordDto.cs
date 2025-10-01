namespace LibraryManagementSystem.Application.Features.BorrowRecord.DTOs
{
    public class BorrowRecordDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserFullName { get; set; } = string.Empty;
        public int BookId { get; set; }
        public string BookName { get; set; } = string.Empty;
        public DateTime BorrowedDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public bool IsReturned => ReturnedDate.HasValue;
    }
}
