using Services;
using Services.Abstraction;
using Shared;

namespace Hotel_Reservation_System.Extentions
{
    public static class CoreExtentions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg => { } ,typeof(Services.AssemblyReferance).Assembly);

            services.AddScoped<IServiceManger, ServiceManger>();

            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            return services;
        }
    }
}
