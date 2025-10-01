using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Infrastructure.Persistence.SeedData
{
    internal class AuthorSeeder : IEntitySeeder
    {
        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            if (!context.Authors.Any())
            {
                context.Authors.AddRange(
                    new Author { Name = "J.K. Rowling" },
                    new Author { Name = "George R.R. Martin" },
                    new Author { Name = "J.R.R. Tolkien" },
                    new Author { Name = "Agatha Christie" },
                    new Author { Name = "Stephen King" }
                );
                await context.SaveChangesAsync();
            }
        }

    }
}
