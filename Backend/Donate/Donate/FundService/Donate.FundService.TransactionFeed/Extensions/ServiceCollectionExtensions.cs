using System;
using Donate.FundService.Data;
using Donate.FundService.TransactionFeed.Services;
using Donate.Shared.QueueListener.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Donate.FundService.TransactionFeed.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabase(this IServiceCollection app, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("SQL_CONNSTR_FUND");
            if (string.IsNullOrWhiteSpace(connectionString))
                connectionString = configuration.GetConnectionString("SQL_CONNSTR_FUND");

            app.AddDbContext<FundContext>(options =>
                options.UseSqlServer(connectionString));
        }

        public static void AddBackgroundService(this IServiceCollection app)
        {
            app.AddHostedService<TransactionGeneratorService>();
        }
    }
}
