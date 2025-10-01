using LibraryManagementSystem.Application.Features.Auth.DTOs;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Contracts.Services
{
    public interface IAuthService
    {
        Task<AuthDto> GetAuthDtoWithNewRefreshTokenAsync(ApplicationUser user);
        Task<AuthDto> GetAuthDtoWithExistingRefreshTokenAsync(ApplicationUser user);
    }
}
