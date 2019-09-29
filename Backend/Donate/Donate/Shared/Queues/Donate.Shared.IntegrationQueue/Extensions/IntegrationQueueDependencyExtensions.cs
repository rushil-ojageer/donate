using Donate.Shared.Queues;
using Microsoft.Extensions.DependencyInjection;

namespace Donate.Shared.IntegrationQueue.Extensions
{
    public static class IntegrationQueueDependencyExtensions
    {
        public static void AddIntegrationQueue<T>(this IServiceCollection app) where T : class, IQueueSettingsResolver
        {
            app.AddSingleton<IQueueSettingsResolver, T>();
            app.AddScoped<IIntegrationEventQueue, IntegrationEventQueue>();
        }


        public static void AddIntegrationQueue<T, TK>(this IServiceCollection app) where T : class, IQueueSettingsResolver where TK : class, IIntegrationEventQueue
        {
            app.AddSingleton<IQueueSettingsResolver, T>();
            app.AddScoped<IIntegrationEventQueue, TK>();
        }
    }
}
