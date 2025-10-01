using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Application.Features.Author.DTOs;

namespace LibraryManagementSystem.Application.Features.Author.Queries.GetAuthorById
{
    public record GetAuthorByIdQuery(int AuthorId) : ICachableRequest<AuthorDto>
    {
        public string CacheKey => CachingKeys.Authors.ById(AuthorId);
    }
}
