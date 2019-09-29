using System;
using Donate.CharityService.API.Data;
using Donate.Shared.API.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ILoggerFactory = Donate.Shared.Logging.ILoggerFactory;
using LoggerFactory = Donate.Shared.Logging.LoggerFactory;

namespace Donate.CharityService.API.Extensions
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

        public static void AddDatabase(this IServiceCollection app, IConfiguration configuration,
            ILogger<Startup> logger)
        {
            var connectionString = Environment.GetEnvironmentVariable("SQL_CONNSTR_CHARITY");
            if (string.IsNullOrWhiteSpace(connectionString))
                connectionString = configuration.GetConnectionString("SQL_CONNSTR_CHARITY");

            app.AddDbContext<CharityContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
