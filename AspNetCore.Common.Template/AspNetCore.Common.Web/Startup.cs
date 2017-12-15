using AspNetCore.Common.Jobs;
using AspNetCore.Common.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace AspNetCore.Common.Web
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
                routes.MapRoute("AccessDenied", "Error/AccessDenied", new { controller = "Shared", action = "AccessDenied" });
                routes.MapRoute("PageNotFound", "404.html", new { controller = "Shared", action = "PageNotFound" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
            
            //FluentScheduler
            //app.UseJob();

            //app.UseHangfire();
            //app.UseHangfireRecurringJobs();
        }
    }
}
