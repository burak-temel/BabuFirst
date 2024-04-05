using Microsoft.AspNetCore.Builder;

namespace Core.Extensions
{
    public static class AppContextMiddlewareExtensions
    {
        public static void ConfigureAppContextMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<AppContextMiddleware>();
        }
    }
}