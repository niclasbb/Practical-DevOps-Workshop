using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCoreSample.Controllers;
using NetCoreSample.Services;

namespace NetCoreSample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Publish options read from configuration file. With that,
            // controllers can use ASP.NET DI to get options (see 
            // BooksController).
            //services.Configure<BooksDemoDataOptions>(
            //    configuration.GetSection("booksDemoDataOptions"));

            services.Configure<BooksDemoDataOptions>(opt => Configuration.GetSection("booksDemoDataOptions"));

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            // Publish singleton for name generator
            services.AddSingleton(typeof(INameGenerator), typeof(NameGenerator));

            // Configure middlewares (CORS and MVC)
            services.AddCors();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                // Running in development mode -> add some dev tools
                app.UseDeveloperExceptionPage();
                app.UseRuntimeInfoPage();
            }

            // Just for demo purposes throw an exception if
            // query string contains "exception"
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Query.ContainsKey("exception"))
            //    {
            //        throw new InvalidOperationException("Something bad happened ...");
            //    }

            //    await next();
            //});

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            // Use file server to serve static files
            app.UseDefaultFiles();
            app.UseFileServer();

            // Add middlewares (CORS and MVC)
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseMvc( routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                }
                );
        }
    }
}
