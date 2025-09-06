using E_Commerce.API.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

namespace E_Commerce.API.Extentions
{
    public static class PresentationExtentions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReferance).Assembly);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });
            services.ConfiguerSwwager();

            services.AddCors(config =>
            {
                config.AddPolicy("CORSPolicy", option =>
                {
                    option.AllowAnyHeader()
                          .AllowAnyMethod()
                          .WithOrigins("http://localhost:4200");
                });
            });

            return services;
        }

        public static IServiceCollection ConfiguerSwwager(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            }

            );
            return services;
        }
    }
}
