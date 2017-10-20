using AspNetCore.Common.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Common.Template
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
            //services.AddDbContext<IdentityDbContext>();

            //services.AddIdentity<AppUser,IdentityRole>(option=> {
            //    option.Password.RequireUppercase = false;
            //    option.Password.RequireDigit = false;
            //    option.Password.RequireLowercase = false;
            //    option.Password.RequireNonAlphanumeric = false;

            //})
            //.AddEntityFrameworkStores<IdentityDbContext>()
            //.AddDefaultTokenProviders();

            //services.AddMvc()
            //    .AddRazorPagesOptions(options =>
            //    {
            //        options.Conventions.AuthorizeFolder("/Account/Manage");
            //        options.Conventions.AuthorizePage("/Account/Logout");
            //    });

            //// Register no-op EmailSender used by account confirmation and password reset during development
            //// For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            //services.AddSingleton<IEmailSender, EmailSender>();

            services.AddConfiguration(Configuration)
                    .AddCommonDbContext()
                    .AddCommonDataProtection()
                    .AddCommonIdentity()
                    .AddCommonMemory(Configuration)
                    .AddCommonServices()
                    .AddCommonCoreMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
