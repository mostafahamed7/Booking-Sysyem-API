
using E_Commerce.API.Extentions;
using Hotel_Reservation_System.Extentions;
using Hotel_Reservation_System.Middelwares;
using System.Threading.Tasks;

namespace Hotel_Booking_System
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configuration Services
            builder.Services.ConfigrationIdentityServices();
            // Infrastructure Services
            builder.Services.AddInfrastructureServices(builder.Configuration);

            // Core Services
            builder.Services.AddCoreServices(builder.Configuration);

            // Presentation Services
            builder.Services.AddPresentationServices();

            builder.Services.AddSwaggerGen(); 


            builder.Services.AddEndpointsApiExplorer();
            #endregion

            var app = builder.Build();

            #region Configration Kestrel Middelware
            await app.SeedDataAsync();
            app.UseMiddleware<GlobalHandlingMiddelware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseCors("CORSPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
