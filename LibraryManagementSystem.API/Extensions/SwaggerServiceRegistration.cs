using LibraryManagementSystem.API.Filters;
using Microsoft.OpenApi.Models;

namespace LibraryManagementSystem.API.Extensions
{
    public static class SwaggerServiceRegistration
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                //c.DocumentFilter<SortPathsDocumentFilter>(); // for Sort Paths Document Filter  
                c.OperationFilter<HideCacheKeysFormDataOperationFilter>();
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Library Management System",
                    Version = "v1",
                    Description = "API documentation for Library Management System",
                    Contact = new OpenApiContact
                    {
                        Name = "Abdelrahman Zagloul",
                        Email = "abdelrahman.zagloul.dev@gmail.com",
                    }

                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
            });

            return services;
        }

    }
}
