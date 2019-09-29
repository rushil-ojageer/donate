using System.IO;
using Donate.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Donate.FundService.Data
{
    public class DesignTimeDbContextFactory : BaseDesignTimeDbContextFactory<FundContext>
    {
        protected override FundContext CreateContext(DbContextOptions<FundContext> options)
        {
            return new FundContext(options);
        }

        protected override string GetConnectionString(IConfigurationRoot configuration)
        {
            return configuration.GetConnectionString("DefaultConnection");
        }

        protected override string GetAppSettingsFileName()
        {
            return "appsettings.json";
        }

        protected override void SqlServerOptionsAction(SqlServerDbContextOptionsBuilder builder)
        {
            base.SqlServerOptionsAction(builder);
            builder.MigrationsAssembly("Donate.FundService.API");
        }
    }
}
