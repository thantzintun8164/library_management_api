using MediatR;

namespace LibraryManagementSystem.Application.Common.Caching
{
    public interface ICachableRequest<TResponse> : IRequest<TResponse>
    {
        string CacheKey { get; }
    }
}
