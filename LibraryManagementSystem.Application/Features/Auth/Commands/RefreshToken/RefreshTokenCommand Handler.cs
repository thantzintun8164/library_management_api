using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Identity;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Contracts.Services;
using LibraryManagementSystem.Application.Features.Auth.DTOs;
using LibraryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Application.Features.Auth.Commands.RefreshToken
{
    internal class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly ICurrentUserService _currentUserService;

        public RefreshTokenCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IAuthService authService, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _authService = authService;
            _currentUserService = currentUserService;
        }

        public async Task<AuthDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnAuthorizedException("User is not authenticated.");

            var user = await _userManager.FindByIdAsync(_currentUserService.UserId!);
            if (user == null)
                throw new UnAuthorizedException("User not found.");

            if (string.IsNullOrEmpty(request.RefreshToken))
                throw new BadRequestException("Refresh token is required.");

            var currentRefreshToken = await _unitOfWork.RefreshTokens.GetAsync(x => x.Token == request.RefreshToken && x.ApplicationUserId == user.Id);
            if (currentRefreshToken == null)
                throw new BadRequestException("invalid refresh Token");

            if (!currentRefreshToken.IsActive)
                throw new BadRequestException("inactive refresh Token");

            currentRefreshToken.RevokedAt = DateTime.UtcNow;
            await _unitOfWork.SaveAsync();

            var authDto = await _authService.GetAuthDtoWithNewRefreshTokenAsync(user);
            return authDto;
        }
    }
}
