using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Application.Features.Book.DTOs;

namespace LibraryManagementSystem.Application.Features.Book.Queries.GetBookById
{
    public record GetBookByIdQuery(int BookId) : ICachableRequest<BookDto>
    {
        public string CacheKey => CachingKeys.Books.ById(BookId);

    }
}
