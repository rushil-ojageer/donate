using System;
using Donate.Shared.Logging;
using Donate.Shared.QueueListener.EventHandler;
using Donate.Shared.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Donate.Shared.QueueListener.Extensions
{
    public static class EventHandlerCollectionExtensions
    {
        public static void AddEventHandlerDependencies(this IServiceCollection app)
        {
            app.AddSingleton<EventHandlerRegistry>();
            app.AddScoped<IEventHandlerCollection, EventHandlerCollection>();
            app.AddTransient<ILoggerFactory, LoggerFactory>();
        }

        public static EventHandlerRegistry GetEventHandlerRegistry(this IApplicationBuilder app)
        {
            return app.ApplicationServices.GetEventHandlerRegistry();
        }

        public static EventHandlerRegistry GetEventHandlerRegistry(this IServiceProvider app)
        {
            return app.GetServiceOrThrow<EventHandlerRegistry>();
        }

        public static void AddEventHandler<T>(this IServiceCollection app) where T : class, IEventHandler
        {
            app.AddScoped<T>();
        }

        public static void UseEventHandler<T>(this IApplicationBuilder app) where T : class, IEventHandler
        {
            var eventHandlerRegistry = app.GetEventHandlerRegistry();
            eventHandlerRegistry.AddEventHandler<T>();
        }
    }
}
