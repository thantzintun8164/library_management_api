using LibraryManagementSystem.Application.Contracts.ExternalService;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Infrastructure.ExternalService
{
    public class BackgroundJobLogger : IBackgroundJobLogger
    {
        private readonly ILogger<BackgroundJobLogger> _logger;
        public BackgroundJobLogger(ILogger<BackgroundJobLogger> logger)
        {
            _logger = logger;
        }
        public Task LogExceptionAsync(string message, string exceptionType, int statusCode, string? stackTrace)
        {
            if (statusCode >= 500)
                _logger.LogError("Unhandled exception ({ExceptionType}) with StatusCode {StatusCode}: {Message}\nStackTrace: {StackTrace}", exceptionType, statusCode, message, stackTrace);
            else
                _logger.LogWarning("Client error ({ExceptionType}) with StatusCode {StatusCode}: {Message}", exceptionType, statusCode, message);

            return Task.CompletedTask;
        }
    }
}
