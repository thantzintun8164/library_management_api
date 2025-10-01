using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Identity;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Application.Features.Auth.Commands.RevokeToken
{
    internal class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public RevokeTokenCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnAuthorizedException("User is not authenticated.");

            if (string.IsNullOrEmpty(request.RefreshToken))
                throw new BadRequestException("Refresh token is required.");

            var user = await _userManager.FindByIdAsync(_currentUserService.UserId!);
            if (user == null)
                throw new NotFoundException("User Not Found.");

            var currentRefreshToken = await _unitOfWork.RefreshTokens.GetAsync(x => x.Token == request.RefreshToken && x.ApplicationUserId == user.Id);
            if (currentRefreshToken == null)
                throw new UnAuthorizedException("Invalid refresh token.");

            if (!currentRefreshToken.IsActive)
                throw new UnAuthorizedException("Inactive refresh token.");

            currentRefreshToken.RevokedAt = DateTime.UtcNow;
            await _unitOfWork.SaveAsync();
            return Unit.Value;

        }
    }
}
