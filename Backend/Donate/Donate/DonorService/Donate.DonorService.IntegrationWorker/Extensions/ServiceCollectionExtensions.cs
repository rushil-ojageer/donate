using System;
using Donate.DonorService.Data;
using Donate.Shared.QueueListener.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Donate.DonorService.IntegrationWorker.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabase(this IServiceCollection app, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("SQL_CONNSTR_DONOR");
            if (string.IsNullOrWhiteSpace(connectionString))
                connectionString = configuration.GetConnectionString("SQL_CONNSTR_DONOR");

            app.AddDbContext<DonorContext>(options =>
                options.UseSqlServer(connectionString));
        }

        public static void AddBackgroundService(this IServiceCollection app)
        {
            app.AddHostedService<QueueListener>();
        }
    }
}
