using Donate.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Donate.DonorService.Data
{
    public class DesignTimeDbContextFactory : BaseDesignTimeDbContextFactory<DonorContext>
    {
        protected override DonorContext CreateContext(DbContextOptions<DonorContext> options)
        {
            return new DonorContext(options);
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
            builder.MigrationsAssembly("Donate.DonorService.API");
        }
    }
}
