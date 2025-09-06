using Domain.Contracts;
using Domain.Entites.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presistance.Data;
using Presistance.Data.Repositories;
using Presistance.Identity;
using Shared;
using StackExchange.Redis;
using System.Text;

namespace Hotel_Reservation_System.Extentions
{
    public static class InfrastructureExtentions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<IdentityAppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReferance).Assembly);

            services.AddSingleton<IConnectionMultiplexer>(services => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            });
            services.AddDistributedMemoryCache();

            services.AddScoped<ICacheRepository, CacheRepository>();

            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }

        public static IServiceCollection ConfigrationIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<IdentityAppDbContext>();
            services.AddScoped<IDbIntializer, DbIntializer>();
            return services;
        }

        public static IServiceCollection ConfigurJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var JwtOption = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option => option.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                // Values
                ValidIssuer = JwtOption.Issuer,
                ValidAudience = JwtOption.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOption.SecretKey))
            }
                );
            services.AddAuthorization();

            return services;
        }

    }
}
