using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Application.Features.Category.DTOs;

namespace LibraryManagementSystem.Application.Features.Category.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(int CategoryId) : ICachableRequest<CategoryDto>
    {
        public string CacheKey => CachingKeys.Categories.ById(CategoryId);

    }
}
