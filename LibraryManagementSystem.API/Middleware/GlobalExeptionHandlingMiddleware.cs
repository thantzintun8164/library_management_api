using LibraryManagementSystem.API.Common.Responses;
using LibraryManagementSystem.Application.Contracts.ExternalService;

namespace LibraryManagementSystem.API.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IBackgroundJobService _backgroundJobService;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, IBackgroundJobService backgroundJobService)
        {
            _next = next;
            _backgroundJobService = backgroundJobService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResponse = ApiErrorFactory.Create(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorResponse.StatusCode;
            _backgroundJobService.Enqueue<IBackgroundJobLogger>(job => job.LogExceptionAsync(exception.Message, exception.GetType().Name, errorResponse.StatusCode, exception.StackTrace));
            await context.Response.WriteAsJsonAsync((object)errorResponse);
        }
    }
}
