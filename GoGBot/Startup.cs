using GoGBot.BLL;
using GoGBot.BLL.Models;
using GoGBot.BLL.Services.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GoGBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(Configuration.GetConnectionString("Hangfire"));
            });

            // Add the processing server as IHostedService
            services.AddHangfireServer(config => config.ServerName = $"{Environment.MachineName}{Guid.NewGuid().ToString().Substring(0, 15)}");
            services.AddControllers().AddNewtonsoftJson();
            services.InitStartup();
            services.BuildServiceProvider().GetRequiredService<IBotService>().GetBotClientAsync().Wait();

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(option =>
            option.AllowAnyOrigin());

            app.UseAuthorization();

            app.UseHangfireServer();

            //add auth for access to dashboard on production
            app.UseHangfireDashboard("/admin/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireDashboardAuthorizationFilter() }
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
