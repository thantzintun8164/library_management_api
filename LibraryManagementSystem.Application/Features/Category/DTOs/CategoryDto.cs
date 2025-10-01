namespace LibraryManagementSystem.Application.Features.Category.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int NumberOfBooks { get; set; }
    }
}
