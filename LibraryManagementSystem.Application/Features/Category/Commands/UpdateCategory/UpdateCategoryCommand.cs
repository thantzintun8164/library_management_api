using LibraryManagementSystem.Application.Common.Caching;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Category.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(int CategoryId, string? Name, string? Description) : IRequest<Unit>, ICacheRemoval
    {
        public string[] CacheKeys =>
        [
            CachingKeys.Categories.All,
            CachingKeys.Categories.ById(CategoryId)
        ];
    }
}
