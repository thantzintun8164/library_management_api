using LibraryManagementSystem.Application.Contracts.Identity;
using LibraryManagementSystem.Application.Features.Account.DTOs;
using LibraryManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LibraryManagementSystem.Infrastructure.Identity
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public bool IsInRole(Role role) => _httpContextAccessor.HttpContext?.User?.IsInRole(role.ToString()) ?? false;
        public IEnumerable<string> Roles => _httpContextAccessor.HttpContext?.User?.Claims
            .Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value) ?? Enumerable.Empty<string>();

        public CurrentUserDto GetCurrentUser()
        {
            return new CurrentUserDto
            {
                UserId = UserId,
                Email = Email,
                IsAuthenticated = IsAuthenticated,
                Roles = Roles
            };
        }

    }
}
