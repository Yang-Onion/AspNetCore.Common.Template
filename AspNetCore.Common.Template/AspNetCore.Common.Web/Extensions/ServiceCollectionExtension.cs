using AspNetCore.Common.Infrastructure;
using AspNetCore.Common.Models.Identity;
using AspNetCore.Common.Template.Data;
using Cms.Service.Identity.Impl;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AspNetCore.Common.Web.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddConfiguration(configuration);
        }

        public static IServiceCollection AddCommonDbContext(this IServiceCollection services)
        {
            return services.AddDbContext<IdentityDbContext>()
                 //添加业务数据库         
                //.AddDbContext<>()
                ;
        }

        public static IServiceCollection AddCommonDataProtection(this IServiceCollection services)
        {
            //services.AddSingleton<IXmlRepository, XmlRepository>();
            services.AddDataProtection().SetApplicationName("AspNetCore.Common");
            services.AddAntiforgery(x => { x.Cookie.Name = "AspNetCore.Common.Antiforgery"; });
            return services;
        }

        public static IServiceCollection AddCommonIdentity(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            })
            .AddCookie(IdentityConstants.ApplicationScheme, option =>
            {
                option.LoginPath = "/Account/Login";
                option.AccessDeniedPath = "/Error/AccessDenied";
                option.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents()
                {
                    OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync
                };
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddScoped<IUserValidator<AppUser>, UserValidator<AppUser>>();
            services.TryAddScoped<IPasswordValidator<AppUser>, PasswordValidator<AppUser>>();
            services.TryAddScoped<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();
            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.TryAddScoped<IdentityErrorDescriber>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<AppUser>, UserClaimsPrincipalFactory<AppUser>>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            });
            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<AppUser>>();


            new IdentityBuilder(typeof(AppUser), typeof(AppRole), services)
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddUserValidator<AppUserValidator>()
                .AddErrorDescriber<IdentityErrorDescriberLocalization>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddCommonMemory(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDistributedRedisCache(option =>
            //{
            //    option.Configuration = configuration.GetConnectionString("RedisConnection");
            //    option.InstanceName = "master";
            //});
            //return services;
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            return services;
        }

        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            //services.AddSingleton<iterface,impl>();
            return services;
        }

        public static IServiceCollection AddCommonCoreMvc(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("aspnetcore.common", builder =>
                {
                    builder.WithOrigins("")
                    .AllowAnyHeader();
                });
            });

            services.Configure<FormOptions>(x => x.MultipartBodyLengthLimit = 1024 * 1024 * 3);
            services.AddMvc(confing =>
            {
                confing.CacheProfiles.Add("Default",
                    new CacheProfile
                    {
                        Duration=3000,
                        Location=Microsoft.AspNetCore.Mvc.ResponseCacheLocation.Any
                    });
                confing.CacheProfiles.Add("Never",

                    new CacheProfile()
                    {
                        Location = Microsoft.AspNetCore.Mvc.ResponseCacheLocation.None,
                        NoStore = true
                    });
                //confing.Filters.Add();
            }).AddJsonOptions(x=>x.SerializerSettings.DateFormatString="yyyy-HH-dd HH:mm");
            return services;
        }
    }
}
