using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Application.Features.Author.DTOs;

namespace LibraryManagementSystem.Application.Features.Author.Queries.GetAllAuthors
{
    public record GetAllAuthorsQuery() : ICachableRequest<List<AuthorDto>>
    {
        public string CacheKey => CachingKeys.Authors.All;
    }
}
