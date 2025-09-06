using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using System.Net;
using System.Text;

namespace Presentation.Controllers
{
    public class RedisCacheAttribute(int durationInSec = 60) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManger>().CacheServices;

            string cacheKey = GenerateCacheKey(context.HttpContext.Request);

            var result = await cacheService.GetCacheValueAsync(cacheKey);

            if (result != null)
            {
                context.Result = new ContentResult
                {
                    Content = result,
                    ContentType = "Application/Json",
                    StatusCode = (int) HttpStatusCode.OK
                };
                return;
            }

            var contantResult = await next.Invoke();

            if(contantResult.Result is OkObjectResult okObject)
            {
                await cacheService.SetCacheValueAsync(cacheKey, okObject, TimeSpan.FromSeconds(durationInSec));
            }
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append(request.Path);

            foreach (var query in request.Query.OrderBy(q => q.Key))
            {
                keyBuilder.Append($"|{query.Key}-{query.Value}");
            }
            
            return keyBuilder.ToString();
        }
    }
}
