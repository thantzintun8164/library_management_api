using LibraryManagementSystem.Application.Contracts.Identity;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Contracts.Services;
using LibraryManagementSystem.Application.Features.Auth.DTOs;
using LibraryManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }
        public async Task<AuthDto> GetAuthDtoWithNewRefreshTokenAsync(ApplicationUser user)
        {
            var jwtToken = await _tokenService.GenerateJwtTokenAsync(user);
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            
            var newRefreshToken = _tokenService.GenerateRefreshToken(user);
            await _unitOfWork.RefreshTokens.AddAsync(newRefreshToken);
            await _unitOfWork.SaveAsync();

            var authDto = new AuthDto
            {
                AccessToken = jwtToken.AccessToken,
                ExpiresAt = jwtToken.ExpiresAt,
                UserId = user.Id,
                Role = role ?? "NR",
                RefreshToken = newRefreshToken,
                RefreshTokenExpiration = newRefreshToken.ExpiresAt
            };
            return authDto;
        }
        public async Task<AuthDto> GetAuthDtoWithExistingRefreshTokenAsync(ApplicationUser user)
        {
            var jwtToken = await _tokenService.GenerateJwtTokenAsync(user);
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            var authDto = new AuthDto
            {
                AccessToken = jwtToken.AccessToken,
                ExpiresAt = jwtToken.ExpiresAt,
                UserId = user.Id,
                Role = role ?? "NR"
            };

            var refreshTokens = await _unitOfWork.RefreshTokens.GetAll(x => x.ApplicationUserId == user.Id).ToListAsync();
            if (refreshTokens.Any(x => x.IsActive))
            {
                var activeRefreshToken = refreshTokens.FirstOrDefault(x => x.IsActive);
                authDto.RefreshToken = activeRefreshToken!;
                authDto.RefreshTokenExpiration = activeRefreshToken!.ExpiresAt;
            }
            else
            {
                var newRefreshToken = _tokenService.GenerateRefreshToken(user);
                await _unitOfWork.RefreshTokens.AddAsync(newRefreshToken);
                await _unitOfWork.SaveAsync();
                authDto.RefreshToken = newRefreshToken;
                authDto.RefreshTokenExpiration = newRefreshToken.ExpiresAt;
            }
            return authDto;
        }
    }
}
