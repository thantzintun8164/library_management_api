using LibraryManagementSystem.Application.Contracts.ExternalService;
using LibraryManagementSystem.Application.Contracts.Services;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Infrastructure.Services
{
    public class AppEmailService : IAppEmailService
    {
        private readonly IEmailService _mailService;
        private readonly IAppEnvironment _appEnvironment;
        public AppEmailService(IEmailService mailService, IAppEnvironment appEnvironment)
        {
            _mailService = mailService;
            _appEnvironment = appEnvironment;
        }

        public async Task SendPasswordChangedEmailAsync(ApplicationUser user)
        {
            var path = Path.Combine(_appEnvironment.WebRootPath, "EmailTemplates", "PasswordChanged.html");
            var templateContent = await File.ReadAllTextAsync(path);
            var emailBody = templateContent
                .Replace("{UserName}", user.FullName);

            await _mailService.SendEmailAsync(user.Email!, "Password Changed Successfully", emailBody, null);
        }

        public async Task SendResetPasswordEmailAsync(ApplicationUser user, string token)
        {
            var path = Path.Combine(_appEnvironment.WebRootPath, "EmailTemplates", "ResetPassword.html");
            var resetLink = $"https://yourapp.com/reset-password?email={user.Email}&token={Uri.EscapeDataString(token)}";// for frontend

            var templateContent = await File.ReadAllTextAsync(path);

            var emailBody = templateContent
                .Replace("{UserName}", user.FullName)
                .Replace("{ResetLink}", resetLink)
                .Replace("{Token}", token);

            await _mailService.SendEmailAsync(user.Email!, "Reset Password", emailBody, null);
        }
    }
}
