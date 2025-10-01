using LibraryManagementSystem.Application.Features.Account.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Account.Queries.GetUserById
{
    public record GetUserByIdQuery(string UserId) : IRequest<UserDto>;
}
