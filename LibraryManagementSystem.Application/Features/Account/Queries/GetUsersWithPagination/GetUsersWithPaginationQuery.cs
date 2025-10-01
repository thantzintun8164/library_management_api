using LibraryManagementSystem.Application.Common.PaginatedResult;
using LibraryManagementSystem.Application.Features.Account.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Account.Queries.GetUsersWithPagination
{
    public record GetUsersWithPaginationQuery(int PageNumber, int PageSize, string BaseUrl) : IRequest<PaginatedResult<UserDto>>, IPaginationQuery;

}

