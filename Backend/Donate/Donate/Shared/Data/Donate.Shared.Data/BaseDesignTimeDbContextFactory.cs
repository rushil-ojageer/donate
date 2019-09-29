using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Donate.Shared.Data
{
    public abstract class BaseDesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
    {
        public T CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(GetAppSettingsFileName())
                .Build();
            var builder = new DbContextOptionsBuilder<T>();
            builder.UseSqlServer(GetConnectionString(configuration), SqlServerOptionsAction);
            return CreateContext(builder.Options);
        }

        protected virtual void SqlServerOptionsAction(SqlServerDbContextOptionsBuilder builder)
        {
        }

        protected abstract T CreateContext(DbContextOptions<T> options);

        protected abstract string GetConnectionString(IConfigurationRoot configuration);

        protected abstract string GetAppSettingsFileName();
    }
}
