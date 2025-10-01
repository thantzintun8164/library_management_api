using LibraryManagementSystem.Application.Common.Caching;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LibraryManagementSystem.Application.Features.Book.Commands.UpdateBookFile
{
    public record UpdateBookFileCommand(int BookId, IFormFile NewFile) : IRequest<Unit>, ICacheRemoval
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
