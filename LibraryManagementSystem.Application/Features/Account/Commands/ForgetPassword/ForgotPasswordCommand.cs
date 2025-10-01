using MediatR;

namespace LibraryManagementSystem.Application.Features.Account.Commands.ForgetPassword
{
    public record ForgotPasswordCommand(string Email) : IRequest<Unit>;

}
