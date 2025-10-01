using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Application.Features.Book.DTOs;

namespace LibraryManagementSystem.Application.Features.Book.Queries.GetAllAvailableBooks
{
    public record GetAllAvailableBooksQuery() : ICachableRequest<List<BookDto>>
    {
        public string CacheKey => CachingKeys.Books.Available;

    }
}
