using LibraryManagementSystem.Application.Features.Auth.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Auth.Commands.Login
{
    public record LoginUserCommand(string Email, string Password) : IRequest<AuthDto>;
}
