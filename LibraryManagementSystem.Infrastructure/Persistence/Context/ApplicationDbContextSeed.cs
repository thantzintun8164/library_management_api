using LibraryManagementSystem.Infrastructure.Persistence.SeedData;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Infrastructure.Persistence.Context
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var seeders = new IEntitySeeder[]
            {
                serviceProvider.GetRequiredService<RoleSeeder>(),
                serviceProvider.GetRequiredService<UserSeeder>(),
                serviceProvider.GetRequiredService<AuthorSeeder>(),
                serviceProvider.GetRequiredService<CategorySeeder>(),
                serviceProvider.GetRequiredService<BookSeeder>()
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(serviceProvider);
            }
        }
    }
}
