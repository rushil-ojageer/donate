using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Donate.Shared.Utilities
{
    public static class ServiceProviderExtensions
    {
        public static T GetServiceOrThrow<T>(this IServiceProvider serviceProvider) where T : class
        {
            var service = serviceProvider.GetService(typeof(T)) as T;
            if (service == null)
                throw new Exception($"Unable to resolve service of type {nameof(T)}.");
            return service;
        }

        public static T GetServiceOrThrow<T>(this IServiceScope serviceProvider) where T : class
        {
            var service = serviceProvider.ServiceProvider.GetService(typeof(T)) as T;
            if (service == null)
                throw new Exception($"Unable to resolve service of type {nameof(T)}.");
            return service;
        }

        public static IServiceProvider GetServiceProvider(this ActionExecutingContext context)
        {
            return context.HttpContext.RequestServices;
        }
    }
}
