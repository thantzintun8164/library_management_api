using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Contracts.Services
{
    public interface IAppEmailService
    {
        Task SendPasswordChangedEmailAsync(ApplicationUser user);
        Task SendResetPasswordEmailAsync(ApplicationUser user, string token);
    }
}
