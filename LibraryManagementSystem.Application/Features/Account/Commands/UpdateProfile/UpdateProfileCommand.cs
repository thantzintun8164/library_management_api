using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Domain.Enums;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Account.Commands.UpdateProfile
{
    public record UpdateProfileCommand(string? FullName, Gender? Gender) : IRequest<Unit>, ICacheRemoval
    {
        public string[] CacheKeys => [CachingKeys.Users.All];
    }
}
