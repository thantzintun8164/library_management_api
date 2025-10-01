using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.ExternalService;
using LibraryManagementSystem.Application.Contracts.Services;
using LibraryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Application.Features.Account.Commands.ResetPassword
{
    internal class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBackgroundJobService _backgroundJobService;

        public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager, IBackgroundJobService backgroundJobService)
        {
            _userManager = userManager;
            _backgroundJobService = backgroundJobService;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new NotFoundException("User not found.");

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"Failed to reset password: {errors}");
            }
            _backgroundJobService.Enqueue<IAppEmailService>(job => job.SendPasswordChangedEmailAsync(user));
            return Unit.Value;
        }
    }
}