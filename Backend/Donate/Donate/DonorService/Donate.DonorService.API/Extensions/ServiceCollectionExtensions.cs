using System;
using Donate.DonorService.API.Settings;
using Donate.DonorService.Data;
using Donate.Shared.API.Extensions;
using Donate.Shared.API.Request;
using Donate.Shared.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Donate.DonorService.API.Extensions
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
            var connectionString = Environment.GetEnvironmentVariable("SQL_CONNSTR_DONOR");
            if (string.IsNullOrWhiteSpace(connectionString))
                connectionString = configuration.GetConnectionString("SQL_CONNSTR_DONOR");

            app.AddDbContext<DonorContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
