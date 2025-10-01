using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Application.Features.Author.DTOs;

namespace LibraryManagementSystem.Application.Features.Author.Queries.GetAuthorsByCategoryName
{
    public record GetAuthorsByCategoryNameQuery(string CategoryName) : ICachableRequest<List<AuthorDto>>
    {
        public string CacheKey => CachingKeys.Authors.ByCategoryName;
    }
}
