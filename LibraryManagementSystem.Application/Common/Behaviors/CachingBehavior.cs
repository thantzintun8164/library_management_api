using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Application.Contracts.ExternalService;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryManagementSystem.Application.Common.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICachableRequest<TResponse>
    {
        private readonly IMemoryCache _cache;
        private readonly IBackgroundJobService _backgroundJobService;
        public CachingBehavior(IMemoryCache cache, IBackgroundJobService backgroundJobService)
        {
            _cache = cache;
            _backgroundJobService = backgroundJobService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var cacheKey = request.CacheKey;

            if (_cache.TryGetValue(cacheKey, out TResponse? cachedResponse) && cachedResponse is not null)
                return cachedResponse;

            var response = await next();

            _backgroundJobService.Enqueue<CachingBehavior<TRequest, TResponse>>(job => job.SetCacheAsync(cacheKey, response));
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            };
            _cache.Set(cacheKey, response, cacheOptions);

            return response;
        }
        public Task SetCacheAsync(string cacheKey, TResponse response)
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            };
            _cache.Set(cacheKey, response, cacheOptions);
            return Task.CompletedTask;
        }
    }
}
