using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Domain.Enums;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Account.Commands.AddToRole
{
    public record AddToRoleCommand(string UserId, Role Role) : IRequest<Unit>, ICacheRemoval
    {
        public string[] CacheKeys => [CachingKeys.Users.All];
    }
}
