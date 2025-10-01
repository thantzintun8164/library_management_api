using LibraryManagementSystem.Application.Common.Caching;
using MediatR;
using Microsoft.AspNetCore.Http;
namespace LibraryManagementSystem.Application.Features.Book.Commands.AddBook
{
    public record AddBookCommand(
        string Name,
        string Description,
        IFormFile BookFile,
        int PublicationYear,
        int NumberOfAvailableBook,
        int AuthorId,
        int CategoryId)
        : IRequest<int>, ICacheRemoval
    {
        public string[] CacheKeys =>
        [
            CachingKeys.Books.ByCategoryName,
            CachingKeys.Books.ByAuthorName,
            CachingKeys.Books.All,
            CachingKeys.Books.Available,
        ];
    }
}

