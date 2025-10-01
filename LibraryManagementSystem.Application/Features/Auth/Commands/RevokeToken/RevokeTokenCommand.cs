using MediatR;

namespace LibraryManagementSystem.Application.Features.Auth.Commands.RevokeToken
{
    public record RevokeTokenCommand(string RefreshToken) : IRequest<Unit>;
}
