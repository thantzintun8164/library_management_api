using MediatR;

namespace LibraryManagementSystem.Application.Features.Account.Commands.ResetPassword
{
    public record ResetPasswordCommand(string Email, string Token, string NewPassword, string ConfirmPassword) : IRequest<Unit>;
}
