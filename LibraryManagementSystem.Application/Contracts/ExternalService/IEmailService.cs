using Microsoft.AspNetCore.Http;

namespace LibraryManagementSystem.Application.Contracts.ExternalService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, List<IFormFile>? attachments = null);
    }
}
