using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Infrastructure.Persistence.SeedData
{
    internal class CategorySeeder : IEntitySeeder
    {
        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Fantasy", Description = "Magic, mythical creatures, and otherworldly adventures." },
                    new Category { Name = "Science Fiction", Description = "Futuristic technology, space exploration, and advanced science." },
                    new Category { Name = "Mystery", Description = "Crime, investigations, and solving thrilling puzzles." },
                    new Category { Name = "History", Description = "Books based on real historical events and figures." },
                    new Category { Name = "Biography", Description = "Life stories of famous and inspiring people." }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
