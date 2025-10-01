using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Application.Features.Account.DTOs;

namespace LibraryManagementSystem.Application.Features.Account.Queries.GetAllUser
{
    public record GetAllUserQuery() : ICachableRequest<List<UserDto>>
    {
        public string CacheKey => CachingKeys.Users.All;
    }
}
