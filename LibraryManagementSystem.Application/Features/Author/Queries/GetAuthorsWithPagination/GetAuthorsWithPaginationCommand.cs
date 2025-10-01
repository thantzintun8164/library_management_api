using LibraryManagementSystem.Application.Common.PaginatedResult;
using LibraryManagementSystem.Application.Features.Author.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Author.Queries.GetAuthorsWithPagination
{
    public record GetAuthorsWithPaginationQuery(int PageNumber, int PageSize, string BaseUrl)
        : IRequest<PaginatedResult<AuthorDto>>, IPaginationQuery;
}
