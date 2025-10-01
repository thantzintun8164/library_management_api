using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Application.Features.Category.DTOs;

namespace LibraryManagementSystem.Application.Features.Category.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery : ICachableRequest<List<CategoryDto>>
    {
        public string CacheKey => CachingKeys.Categories.All;

    }
}
