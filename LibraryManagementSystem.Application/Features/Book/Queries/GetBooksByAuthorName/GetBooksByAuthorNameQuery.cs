using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Application.Features.Book.DTOs;

namespace LibraryManagementSystem.Application.Features.Book.Queries.GetBooksByAuthorName
{
    public record GetBooksByAuthorNameQuery(string AuthorName) : ICachableRequest<List<BookDto>>
    {
        public string CacheKey => CachingKeys.Books.ByAuthorName;
    }
}
