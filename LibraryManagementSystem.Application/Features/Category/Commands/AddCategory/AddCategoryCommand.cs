using LibraryManagementSystem.Application.Common.Caching;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Category.Commands.AddCategory
{
    public record AddCategoryCommand(string Name, string? Description) : IRequest<int>, ICacheRemoval
    {
        public string[] CacheKeys =>
        [
            CachingKeys.Categories.All
        ];
    }
}
