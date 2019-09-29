using Donate.DonorService.API.Extensions;
using Donate.DonorService.API.Settings;
using Donate.Shared.API.Extensions;
using Donate.Shared.API.Filters;
using Donate.Shared.IntegrationQueue.Extensions;
using Donate.Shared.IntegrationQueue.Settings;
using Donate.Shared.Queues.Extensions;
using Donate.Shared.Queues.Settings;
using Donate.Shared.ResponseQueue.Extensions;
using Donate.Shared.ResponseQueue.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Donate.DonorService.API
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private const string CorsOriginPolicy = "CorsOriginPolicy";

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy(CorsOriginPolicy, builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4200", "http://34.90.158.29")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            services.AddMvc(SetCustomMvcOptions)
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Settings
            services.ConfigureQueueSettings(Configuration.GetSection("QueueConnection"), _env);
            services.Configure<IntegrationEventQueueSettings>(Configuration.GetSection("IntegrationEventQueueSettings"));
            services.Configure<ResponseQueueSettings>(Configuration.GetSection("ResponseQueueSettings"));

            //Queues
            services.AddQueueConnectionManager();
            services.AddQueueChannel(ServiceLifetime.Scoped);
            services.AddQueueFactory(ServiceLifetime.Scoped);
            services.AddResponseQueue<QueueSettingsResolver>();
            services.AddIntegrationQueue<QueueSettingsResolver>();

            //Dependencies
            services.AddDependencies();

            //Database
            services.AddDatabase(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseHttpStatusCodeExceptionMiddleware();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpStatusCodeExceptionMiddleware();
            }

            app.UseCors(CorsOriginPolicy);
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void SetCustomMvcOptions(MvcOptions options)
        {
            options.Filters.Add(typeof(RequestContextFilter));
            options.Filters.Add(typeof(AsynchronousActionExecutionFilter));
        }
    }
}
