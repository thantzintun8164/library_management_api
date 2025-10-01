namespace LibraryManagementSystem.Application.Features.Account.DTOs
{
    public class CurrentUserDto
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public bool IsAuthenticated { get; set; }
        public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
    }
}
