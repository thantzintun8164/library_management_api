using LibraryManagementSystem.Infrastructure.Persistence.Context;

namespace LibraryManagementSystem.API.Extensions
{
    public static class DatabaseSeederExtensions
    {
        public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            await ApplicationDbContextSeed.SeedAsync(services);
        }
    }
}
