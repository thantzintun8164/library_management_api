using LibraryManagementSystem.Application.Common.Caching;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Author.Commands.AddAuthor
{
    public record AddAuthorCommand(string Name) : IRequest<int>, ICacheRemoval
    {
        public string[] CacheKeys =>
        [
            CachingKeys.Authors.All,
            CachingKeys.Authors.ByCategoryName
        ];
    }
}
