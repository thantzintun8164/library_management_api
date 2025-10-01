using MediatR;

namespace LibraryManagementSystem.Application.Features.Account.Commands.ChangePassword
{
    public record ChangePasswordCommand(string CurrentPassword, string NewPassword, string ConfirmNewPassword) : IRequest<Unit>;
}
