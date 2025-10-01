using LibraryManagementSystem.Application.Contracts.ExternalService;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace LibraryManagementSystem.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>

    {
        private readonly IBackgroundJobService _backgroundJobService;
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(IBackgroundJobService backgroundJobService, ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _backgroundJobService = backgroundJobService;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();
            var response = await next();
            stopwatch.Stop();
            var requestName = typeof(TRequest).Name;
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            _backgroundJobService.Enqueue<LoggingBehavior<TRequest, TResponse>>(job => job.LogRequestAsync(requestName, elapsedMilliseconds));
            return response;

        }

        public Task LogRequestAsync(string requestName, long elapsedMilliseconds)
        {
            _logger.LogInformation("Background Job: Handled {RequestName} in {ElapsedMilliseconds}ms", requestName, elapsedMilliseconds);
            return Task.CompletedTask;
        }
    }
}
