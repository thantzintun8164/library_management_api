namespace LibraryManagementSystem.Application.Features.Book.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public int NumberOfAvailableBook { get; set; }
        public bool IsAvailable => NumberOfAvailableBook > 0;
        public string BookFileUrl { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int NumberOfBorrowRecords { get; set; }
    }
}
