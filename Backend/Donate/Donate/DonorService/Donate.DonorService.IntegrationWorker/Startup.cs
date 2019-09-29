using Donate.DonorService.IntegrationWorker.EventHandlers;
using Donate.DonorService.IntegrationWorker.Extensions;
using Donate.DonorService.IntegrationWorker.Settings;
using Donate.Shared.IntegrationQueue.Extensions;
using Donate.Shared.IntegrationQueue.Settings;
using Donate.Shared.QueueListener.Extensions;
using Donate.Shared.Queues.Extensions;
using Donate.Shared.Queues.Settings;
using Donate.Shared.ResponseQueue.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Donate.DonorService.IntegrationWorker
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
            services.Configure<IntegrationEventQueueSettings>(Configuration.GetSection("IntegrationEventQueueSettings"));

            //Queues
            services.AddQueueConnectionManager();
            services.AddQueueChannel();
            services.AddQueueFactory();
            services.AddIntegrationQueue<QueueSettingsResolver>();
            services.AddEventHandlerDependencies();
            services.AddEventHandler<AddCharityEventHandler>();
            services.AddEventHandler<RemoveCharityEventHandler>();
            services.AddEventHandler<UpdateCharityEventHandler>();
            services.AddEventHandler<NewTransactionEventHandler>();

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
                await context.Response.WriteAsync("Donate.DonorService.IntegrationWorker");
            });

            app.UseEventHandler<AddCharityEventHandler>();
            app.UseEventHandler<RemoveCharityEventHandler>();
            app.UseEventHandler<UpdateCharityEventHandler>();
            app.UseEventHandler<NewTransactionEventHandler>();
        }
    }
}
