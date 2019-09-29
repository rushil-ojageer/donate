using Donate.Shared.API.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Donate.Shared.API.Extensions
{
    public static class HttpStatusCodeExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpStatusCodeExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpStatusCodeExceptionMiddleware>();
        }
    }
}