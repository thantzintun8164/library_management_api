using LibraryManagementSystem.Application.Common.Caching;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Commands.UpdateAvailableBook
{
    public record UpdateAvailableBookCommand(int BookId, int NumberOfAvailableBook) : IRequest<Unit>, ICacheRemoval
    {
        public string[] CacheKeys =>
        [
            CachingKeys.Books.ByCategoryName,
            CachingKeys.Books.ByAuthorName,
            CachingKeys.Books.All,
            CachingKeys.Books.Available,
            CachingKeys.Books.ById(BookId),
        ];
    }
}
