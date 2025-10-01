using LibraryManagementSystem.Application.Features.Account.DTOs;
using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Application.Contracts.Identity
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? Email { get; }
        bool IsAuthenticated { get; }
        bool IsInRole(Role role);
        IEnumerable<string> Roles { get; }
        CurrentUserDto GetCurrentUser();
    }
}
