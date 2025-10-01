using LibraryManagementSystem.Application.Common.Caching;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Commands.UpdateBook
{
    public record UpdateBookCommand(int BookId, string? Name, string? Description, int? PublicationYear) : IRequest<Unit>, ICacheRemoval
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
