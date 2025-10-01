using LibraryManagementSystem.Application.Common.PaginatedResult;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Queries.GetBooksWithPagination
{
    public record GetBooksWithPaginationQuery(int PageNumber, int PageSize, string BaseUrl)
        : IRequest<PaginatedResult<BookDto>>, IPaginationQuery;

}
