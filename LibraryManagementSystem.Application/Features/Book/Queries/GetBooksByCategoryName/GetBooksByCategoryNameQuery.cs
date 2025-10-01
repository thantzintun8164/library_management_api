using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Application.Features.Book.DTOs;

namespace LibraryManagementSystem.Application.Features.Book.Queries.GetBooksByCategoryName
{
    public record GetBooksByCategoryNameQuery(string CategoryName) : ICachableRequest<List<BookDto>>
    {
        public string CacheKey => CachingKeys.Books.ByCategoryName;

    }
}
