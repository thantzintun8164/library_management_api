using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Application.Contracts.ExternalService;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryManagementSystem.Application.Common.Behaviors
{
    public class CacheRemovalBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheRemoval
    {
        private readonly IBackgroundJobService _backgroundJobService;
        private readonly IMemoryCache _cache;

        public CacheRemovalBehavior(IBackgroundJobService backgroundJobService, IMemoryCache cache)
        {
            _backgroundJobService = backgroundJobService;
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();
            string[] cacheKeys = request.CacheKeys;
            _backgroundJobService.Enqueue<CacheRemovalBehavior<TRequest, TResponse>>(job => job.RemoveCacheAsync(cacheKeys));
            return response;
        }

        public Task RemoveCacheAsync(string[] cacheKeys)
        {
            if (cacheKeys == null || cacheKeys.Length == 0)
                return Task.CompletedTask;

            foreach (var key in cacheKeys)
            {
                if (!string.IsNullOrWhiteSpace(key))
                    _cache.Remove(key);
            }
            return Task.CompletedTask;
        }
    }
}

