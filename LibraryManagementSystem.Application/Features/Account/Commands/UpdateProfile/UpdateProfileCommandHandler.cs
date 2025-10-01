using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Identity;
using LibraryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.Account.Commands.UpdateProfile
{
    internal class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public UpdateProfileCommandHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnAuthorizedException("User is not authenticated.");
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == _currentUserService.UserId);

            if (user is null)
                throw new UnAuthorizedException("User not found.");

            if (!string.IsNullOrWhiteSpace(request.FullName))
                user.FullName = request.FullName;

            if (request.Gender.HasValue)
                user.Gender = request.Gender.Value;

            await _userManager.UpdateAsync(user);
            return Unit.Value;
        }
    }
}
