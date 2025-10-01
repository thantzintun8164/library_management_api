using LibraryManagementSystem.Application.Common.Caching;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Commands.RemoveBook
{
    public record RemoveBookCommand(int BookId) : IRequest<Unit>, ICacheRemoval
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
