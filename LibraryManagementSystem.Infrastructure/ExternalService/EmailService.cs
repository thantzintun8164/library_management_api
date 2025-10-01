using LibraryManagementSystem.Application.Contracts.ExternalService;
using LibraryManagementSystem.Application.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace LibraryManagementSystem.Infrastructure.ExternalService
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value ?? throw new ArgumentNullException(nameof(emailSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }



        public async Task SendEmailAsync(string to, string subject, string body, List<IFormFile>? attachments)
        {
            using var message = await CreateEmailMessage(to, subject, body, attachments);

            using var smtpClient = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password)
            };

            try
            {
                await smtpClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {To}", to);
                throw new InvalidOperationException("Failed to send email", ex);
            }
        }
        private async Task<MailMessage> CreateEmailMessage(string to, string subject, string body, List<IFormFile>? attachments)
        {
            var message = new MailMessage
            {
                From = new MailAddress(_emailSettings.Email, _emailSettings.DisplayName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            message.To.Add(new MailAddress(to));

            // Add attachments
            if (attachments != null && attachments.Any())
            {
                foreach (var file in attachments.Where(x => x.Length > 0))
                {
                    var stream = new MemoryStream();
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    message.Attachments.Add(new Attachment(stream, file.FileName, file.ContentType));
                }
            }
            return message;
        }
    }
}
