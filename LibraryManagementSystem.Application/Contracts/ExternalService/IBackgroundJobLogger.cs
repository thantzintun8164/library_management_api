namespace LibraryManagementSystem.Application.Contracts.ExternalService
{

    public interface IBackgroundJobLogger
    {
        Task LogExceptionAsync(string message, string exceptionType, int statusCode, string? stackTrace);
    }
}
