using LibraryManagementSystem.Application.Features.Auth.DTOs;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Contracts.Identity
{
    public interface ITokenService
    {
        Task<JwtTokenDto> GenerateJwtTokenAsync(ApplicationUser user);
        RefreshToken GenerateRefreshToken(ApplicationUser user);
    }
}
