using System;
using Microsoft.AspNetCore.Http;

namespace Donate.Shared.API.Extensions
{
    public static class HttpContextExtensions
    {

        public static IHeaderDictionary GetHeaders(this HttpContext context)
        {
            return context.Request.Headers;
        }

        public static Guid? GetGuidOrDefault(this IHeaderDictionary headers, string key, Guid? defaultValue = null)
        {
            if (!headers.TryGetValue(key, out var valueString) && Guid.TryParse(valueString, out var valueGuid))
            {
                return valueGuid;
            }

            return defaultValue;
        }
    }
}
