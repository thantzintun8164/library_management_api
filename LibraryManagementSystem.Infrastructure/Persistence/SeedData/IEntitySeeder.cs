namespace LibraryManagementSystem.Infrastructure.Persistence.SeedData
{
    public interface IEntitySeeder
    {
        Task SeedAsync(IServiceProvider serviceProvider);
    }
}
