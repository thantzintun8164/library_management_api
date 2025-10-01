using LibraryManagementSystem.Application.Common.PaginatedResult;
using LibraryManagementSystem.Application.Features.BorrowRecord.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetBorrowedBooksWithPagination
{
    public record GetBorrowedBooksWithPaginationQuery(int PageNumber, int PageSize, string BaseUrl)
        : IRequest<PaginatedResult<BorrowRecordDto>>, IPaginationQuery;
}
