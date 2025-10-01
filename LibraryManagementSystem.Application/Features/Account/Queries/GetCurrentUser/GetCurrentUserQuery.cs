using LibraryManagementSystem.Application.Features.Account.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Account.Queries.GetCurrentUser
{
    public record GetCurrentUserQuery() : IRequest<UserDto>;
}
