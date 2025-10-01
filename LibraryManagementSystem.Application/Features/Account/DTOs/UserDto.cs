using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Application.Features.Account.DTOs
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public Role Role { get; set; }
    }
}
