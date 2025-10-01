using LibraryManagementSystem.API.Common.Services;
using LibraryManagementSystem.Application.Contracts.Services;
using LibraryManagementSystem.Application.Settings;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

namespace LibraryManagementSystem.API.Extensions
{
    public static class ApiServiceRegistration
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureControllersAndSwagger(services);
            RegisterRateLimiter(services);
            ConfigureSerilog();
            ConfigureCors(services);
            RegisterServicesAndSettings(services, configuration);

            return services;
        }

        private static void ConfigureControllersAndSwagger(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerDocumentation();
        }

        private static void RegisterRateLimiter(IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("FixedWindow", opt =>
                {
                    opt.PermitLimit = 60;
                    opt.Window = TimeSpan.FromMinutes(1);
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 0;
                });
            });
        }
        private static void ConfigureSerilog()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(
                    "Logs/log-.txt",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 10 * 1024 * 1024, // 10MB
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: 10,
                    shared: true
                )
                .CreateLogger();
        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CORS", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }

        private static void RegisterServicesAndSettings(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAppEnvironment, AppEnvironment>();
            services.Configure<FileStorageSettings>(configuration.GetSection("FileStorageSettings"));
            services.Configure<JwtTokenSettings>(configuration.GetSection("JWTSettings"));
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<DefaultUsersSettings>(configuration.GetSection("DefaultUsers"));
        }
    }
}