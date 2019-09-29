using System;
using Donate.FundService.Data;
using Donate.Shared.API.Request;
using Donate.Shared.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Donate.FundService.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependencies(this IServiceCollection app)
        {
            // Transient Dependencies
            app.AddTransient<ILoggerFactory, LoggerFactory>();

            // Scoped Dependencies
            app.AddScoped<IRequestContext, RequestContext>();
        }

        public static void AddDatabase(this IServiceCollection app, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("SQL_CONNSTR_FUND");
            if (string.IsNullOrWhiteSpace(connectionString))
                connectionString = configuration.GetConnectionString("SQL_CONNSTR_FUND");

            app.AddDbContext<FundContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
