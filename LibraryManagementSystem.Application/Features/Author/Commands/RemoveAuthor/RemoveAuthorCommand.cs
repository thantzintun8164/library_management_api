using LibraryManagementSystem.Application.Common.Caching;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Author.Commands.RemoveAuthor
{
    public record RemoveAuthorCommand(int AuthorId) : IRequest<Unit>, ICacheRemoval
    {
        public string[] CacheKeys =>
        [
            CachingKeys.Authors.All,
            CachingKeys.Authors.ByCategoryName,
            CachingKeys.Authors.ById(AuthorId)
        ];
    }
}
