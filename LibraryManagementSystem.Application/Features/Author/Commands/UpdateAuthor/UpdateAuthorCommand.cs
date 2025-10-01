using LibraryManagementSystem.Application.Common.Caching;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Author.Commands.UpdateAuthor
{
    public record UpdateAuthorCommand(int AuthorId, string Name) : IRequest<Unit>, ICacheRemoval
    {
        public string[] CacheKeys =>
        [
            CachingKeys.Authors.All,
            CachingKeys.Authors.ByCategoryName,
            CachingKeys.Authors.ById(AuthorId)
        ];
    }
}
