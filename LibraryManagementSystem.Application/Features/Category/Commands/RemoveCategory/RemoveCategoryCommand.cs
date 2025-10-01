using LibraryManagementSystem.Application.Common.Caching;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Category.Commands.RemoveCategory
{
    public record RemoveCategoryCommand(int CategoryId) : IRequest<Unit>, ICacheRemoval
    {
        public string[] CacheKeys =>
        [
            CachingKeys.Categories.All,
            CachingKeys.Categories.ById(CategoryId)
        ];
    }
}
