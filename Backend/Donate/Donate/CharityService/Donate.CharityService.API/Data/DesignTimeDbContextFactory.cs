using System.IO;
using Donate.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Donate.CharityService.API.Data
{
    public class DesignTimeDbContextFactory : BaseDesignTimeDbContextFactory<CharityContext>
    {
        protected override CharityContext CreateContext(DbContextOptions<CharityContext> options)
        {
            return new CharityContext(options);
        }

        protected override string GetConnectionString(IConfigurationRoot configuration)
        {
            return configuration.GetConnectionString("DefaultConnection");
        }

        protected override string GetAppSettingsFileName()
        {
            return "appsettings.json";
        }
    }
}
