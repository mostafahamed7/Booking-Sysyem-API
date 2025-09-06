using Domain.Contracts;

namespace E_Commerce.API.Extentions
{
    public static class WebApplicationExtentions
    {
        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();
                await dbInitializer.InitializeIdentityAsync();
            }

            return app;
        }
        
    }
}
