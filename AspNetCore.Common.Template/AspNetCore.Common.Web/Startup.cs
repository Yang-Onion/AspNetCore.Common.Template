using AspNetCore.Common.Jobs;
using AspNetCore.Common.Services.Jobs;
using AspNetCore.Common.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace AspNetCore.Common.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                    .AddConfiguration(Configuration)
                    .AddCommonDbContext(Configuration)
                    .AddCommonDataProtection()
                    .AddCommonIdentity()
                    .AddCommonMemory(Configuration)
                    .AddCommonServices()
                    .AddCommonCoreMvc()
                    .AddCommonJobs(Configuration);

        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            loggerFactory.AddNLog();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
            //FluentScheduler
            //app.UseJob();
            app.UseHangfire();
            app.UseHangfireRecurringJobs();
        }
    }
}
