using LibraryManagementSystem.Application.Contracts.ExternalService;
using LibraryManagementSystem.Application.Contracts.Services;
using LibraryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Application.Features.Account.Commands.ForgetPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBackgroundJobService _backgroundJobService;

        public ForgotPasswordCommandHandler(UserManager<ApplicationUser> userManager, IBackgroundJobService backgroundJobService)
        {
            _userManager = userManager;
            _backgroundJobService = backgroundJobService;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Unit.Value;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            _backgroundJobService.Enqueue<IAppEmailService>(job => job.SendResetPasswordEmailAsync(user, token));

            return Unit.Value;
        }

    }
}
