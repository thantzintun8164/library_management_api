using Hangfire;
using LibraryManagementSystem.API.Extensions;
using LibraryManagementSystem.API.Middleware;
using LibraryManagementSystem.Application.Extensions;
using LibraryManagementSystem.Infrastructure.Extensions;
using Serilog;
namespace LibraryManagementSystem.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddApiServices(builder.Configuration)
                .AddApplicationServices(builder.Configuration)
                .AddInfrastructureServices(builder.Configuration);

            builder.Host.UseSerilog();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRateLimiter();
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            app.UseHangfireDashboard("/hangfire");
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors("CORS");

            await app.SeedDatabaseAsync();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
