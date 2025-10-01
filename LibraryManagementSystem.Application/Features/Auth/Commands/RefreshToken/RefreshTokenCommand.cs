using LibraryManagementSystem.Application.Features.Auth.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<AuthDto>;
}
