using System;
using System.Collections.Generic;
using System.Text;
using Donate.Shared.Queues;
using Microsoft.Extensions.DependencyInjection;

namespace Donate.Shared.ResponseQueue.Extensions
{
    public static class ResponseQueueDependencyExtensions
    {
        public static void AddResponseQueue<T>(this IServiceCollection app) where T : class, IQueueSettingsResolver
        {
            app.AddSingleton<IQueueSettingsResolver, T>();
            app.AddScoped<IResponseQueue, ResponseQueue>();
        }
    }
}
