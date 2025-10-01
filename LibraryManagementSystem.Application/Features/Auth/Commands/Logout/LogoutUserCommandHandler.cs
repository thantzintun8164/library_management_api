using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Identity;
using LibraryManagementSystem.Application.Contracts.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.Auth.Commands.Logout
{
    internal class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public LogoutUserCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnAuthorizedException("User is not authenticated.");

            var refreshTokens = await _unitOfWork.RefreshTokens
                .GetAll(x => x.ApplicationUserId == _currentUserService.UserId)
                .ToListAsync(cancellationToken);

            if (refreshTokens.Any())
            {
                _unitOfWork.RefreshTokens.RemoveRange(refreshTokens);
                await _unitOfWork.SaveAsync();
            }
            return Unit.Value;
        }
    }
}
