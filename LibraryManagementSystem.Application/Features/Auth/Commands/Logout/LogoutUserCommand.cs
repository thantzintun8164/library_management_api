using MediatR;

namespace LibraryManagementSystem.Application.Features.Auth.Commands.Logout
{
    public record LogoutUserCommand() : IRequest<Unit>;
}
