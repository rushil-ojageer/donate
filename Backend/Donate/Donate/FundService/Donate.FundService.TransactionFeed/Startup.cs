using Donate.FundService.TransactionFeed.Extensions;
using Donate.FundService.TransactionFeed.Settings;
using Donate.Shared.IntegrationQueue.Extensions;
using Donate.Shared.Logging;
using Donate.Shared.Queues.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Donate.FundService.TransactionFeed
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Settings
            services.ConfigureQueueSettings(Configuration.GetSection("QueueConnection"), _env);
            services.Configure<TransactionListenerSettings>(Configuration.GetSection("TransactionListenerSettings"));

            //Queues
            services.AddQueueConnectionManager();
            services.AddQueueChannel();
            services.AddQueueFactory();
            services.AddIntegrationQueue<QueueSettingsResolver>();
            services.AddTransient<ILoggerFactory, LoggerFactory>(); 
            services.AddTransient<TransactionListenerSettings>();

            services.AddBackgroundService();
            services.AddDatabase(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Donate.FundService.TransactionFeed");
            });
        }
    }
}
