using Microsoft.AspNetCore.Builder;

namespace Recipe.ExceptionHandler
{
    public static class CustomExceptionMiddlewareConfig
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
