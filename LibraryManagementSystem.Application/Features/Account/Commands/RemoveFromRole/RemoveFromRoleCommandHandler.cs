using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Identity;
using LibraryManagementSystem.Application.Settings;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace LibraryManagementSystem.Application.Features.Account.Commands.RemoveFromRole
{
    internal class RemoveFromRoleCommandHandler : IRequestHandler<RemoveFromRoleCommand, Unit>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DefaultUsersSettings _settings;
        public RemoveFromRoleCommandHandler(ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager, IOptions<DefaultUsersSettings> settings)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
            _settings = settings.Value;
        }
        public async Task<Unit> Handle(RemoveFromRoleCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsInRole(Role.Admin))
                throw new ForbiddenException("Only Admins can remove roles.");

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                throw new NotFoundException($"User with ID {request.UserId} not found.");
            if (user.Email == _settings.AdminEmail)
                throw new BadRequestException("Cannot remove role from the default admin user.");

            var roleName = request.Role.ToString();
            if (!await _userManager.IsInRoleAsync(user, roleName))
                throw new BadRequestException($"User is not in role {roleName}.");

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"Failed to remove role: {errors}");
            }
            if (request.Role == Role.Admin)
                user.Role = Role.User;

            await _userManager.UpdateAsync(user);
            return Unit.Value;
        }
    }
}
