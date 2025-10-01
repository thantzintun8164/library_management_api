using LibraryManagementSystem.Application.Common.PaginatedResult;
using LibraryManagementSystem.Application.Features.Category.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Category.Queries.GetCategoriesWithPagination
{
    public record GetCategoriesWithPaginationQuery(int PageNumber, int PageSize, string BaseUrl)
        : IRequest<PaginatedResult<CategoryDto>>, IPaginationQuery;
}
