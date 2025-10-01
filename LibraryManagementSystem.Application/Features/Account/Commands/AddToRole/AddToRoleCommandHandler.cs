using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Identity;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Application.Features.Account.Commands.AddToRole
{
    internal class AddToRoleCommandHandler : IRequestHandler<AddToRoleCommand, Unit>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddToRoleCommandHandler(ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(AddToRoleCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsInRole(Role.Admin))
                throw new ForbiddenException("Only Admins can assign roles.");

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                throw new NotFoundException($"User with ID {request.UserId} not found.");

            var roleName = request.Role.ToString();
            if (await _userManager.IsInRoleAsync(user, roleName))
                throw new BadRequestException($"User is already in role {roleName}.");

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"Failed to add role: {errors}");
            }

            user.Role = request.Role;
            await _userManager.UpdateAsync(user);
            return Unit.Value;
        }
    }
}
