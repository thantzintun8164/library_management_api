using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Application.Features.Book.DTOs;

namespace LibraryManagementSystem.Application.Features.Book.Queries.GetAllBooks
{
    public record GetAllBooksQuery() : ICachableRequest<List<BookDto>>
    {
        public string CacheKey => CachingKeys.Books.All;
    }
}
