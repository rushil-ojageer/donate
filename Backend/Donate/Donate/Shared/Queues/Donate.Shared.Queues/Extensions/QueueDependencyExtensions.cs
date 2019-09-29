using System;
using Donate.Shared.Logging;
using Donate.Shared.Queues.Settings;
using Donate.Shared.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Donate.Shared.Queues.Extensions
{
    public static class QueueDependencyExtensions
    {
        public static void ConfigureQueueSettings(this IServiceCollection app, IConfigurationSection section, IHostingEnvironment environment)
        {
            app.Configure<QueueConnectionSettings>(settings =>
            {
                settings.MaxConnectionRetries = section.GetValue<int>("MaxConnectionRetries");
                settings.ConnectionRetryWaitSeconds = section.GetValue<int>("ConnectionRetryWaitSeconds");

                if (environment.IsDevelopment())
                {
                    settings.HostName = section.GetValue<string>("HostName");
                    settings.Port = section.GetValue<int>("Port");
                    settings.Username = section.GetValue<string>("Username");
                    settings.Password = section.GetValue<string>("Password");
                }
                else
                {
                    settings.HostName = GetEnvironmentVariableOrThrow<string>("RMQ_HOSTNAME");
                    settings.Port = GetEnvironmentVariableOrThrow<int>("RMQ_PORT");
                    settings.Username = GetEnvironmentVariableOrThrow<string>("RMQ_USERNAME");
                    settings.Password = GetEnvironmentVariableOrThrow<string>("RMQ_PASSWORD");
                }
            });
        }

        private static T GetEnvironmentVariableOrThrow<T>(string key)
        {
            var value = Environment.GetEnvironmentVariable(key);

            if (value == null)
                throw new Exception($"Cannot find environment variable '{key}'");

            return (T) Convert.ChangeType(value, typeof(T));
        }

        public static void AddQueueConnectionManager(this IServiceCollection app)
        {
            app.AddSingleton(CreateQueueConnectionManager);
        }

        public static void AddQueueChannel(this IServiceCollection app, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            if (serviceLifetime == ServiceLifetime.Scoped)
            {
                app.AddScoped<IQueueChannelManager, QueueChannelManager>();
                return;
            }

            if (serviceLifetime == ServiceLifetime.Transient)
            {
                app.AddTransient<IQueueChannelManager, QueueChannelManager>();
                return;
            }

            throw new Exception($"Cannot register service {nameof(IQueueChannelManager)} using the '{serviceLifetime}' service lifetime.");
        }

        public static void AddQueueFactory(this IServiceCollection app, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            if (serviceLifetime == ServiceLifetime.Scoped)
            {
                app.AddScoped<IQueueFactory, QueueFactory>();
                return;
            }

            if (serviceLifetime == ServiceLifetime.Transient)
            {
                app.AddTransient<IQueueFactory, QueueFactory>();
                return;
            }

            throw new Exception($"Cannot register service {nameof(IQueueFactory)} using the '{serviceLifetime}' service lifetime.");
        }

        private static IQueueConnectionManager CreateQueueConnectionManager(IServiceProvider provider)
        {
            var loggerFactory = provider.GetServiceOrThrow<ILoggerFactory>();
            var settings = provider.GetServiceOrThrow<IOptions<QueueConnectionSettings>>();
            return new QueueConnectionManager(settings, loggerFactory);
        }
    }
}
