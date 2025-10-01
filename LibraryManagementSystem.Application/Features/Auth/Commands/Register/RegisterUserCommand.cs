using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Application.Features.Auth.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Auth.Commands.Register
{
    public record RegisterUserCommand(string FullName, string Email, string Password, string ConfirmPassword) : IRequest<AuthDto>, ICacheRemoval
    {
        public string[] CacheKeys => [CachingKeys.Users.All];
    }
}
