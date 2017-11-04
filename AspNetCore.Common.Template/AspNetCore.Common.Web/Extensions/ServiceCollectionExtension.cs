using AspNetCore.Common.Infrastructure;
using AspNetCore.Common.Infrastructure.Data;
using AspNetCore.Common.Infrastructure.Extension;
using AspNetCore.Common.Infrastructure.Web;
using AspNetCore.Common.Models.Identity;
using AspNetCore.Common.Services;
using AspNetCore.Common.Services.Identity.Impl;
using AspNetCore.Common.Web.Conventions;
using AspNetCore.Common.Web.Providers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace AspNetCore.Common.Web.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSingleton(configuration);
        }

        public static IServiceCollection AddCommonDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<IdentityDbContext>()
                            //添加业务库
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
            services.AddScoped<OpenIdOAuthProvider>();
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
                option.Events = new CookieAuthenticationEvents()
                {
                    OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync
                };
            })
            //第三方平台调用当前平台时需要验证 
            .AddOAuthValidation()
            .AddOpenIdConnectServer(options =>
            {
                options.AllowInsecureHttp = true;
                options.TokenEndpointPath = "/oauth/token";
                options.LogoutEndpointPath = "/oauth/logout";
                options.ApplicationCanDisplayErrors = true;
                options.AccessTokenLifetime = TimeSpan.FromDays(1);
                options.ProviderType = typeof(OpenIdOAuthProvider);
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

            services.TryAddScoped<UserManager<AppUser>, AppUserManager>();
            services.TryAddScoped<SignInManager<AppUser>, SignInManager>();
            services.TryAddScoped<RoleManager<AppRole>, AppRoleManager>();

            new IdentityBuilder(typeof(AppUser), typeof(AppRole), services)
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddUserValidator<AppUserValidator>()
                .AddErrorDescriber<IdentityErrorDescriberLocalization>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddCommonMemory(this IServiceCollection services, IConfiguration configuration)
        {
            //注入redis分布式缓存
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = configuration.GetConnectionString("RedisConnection");
                option.InstanceName = "master";
            });
            return services.AddMemoryCache();
        }

        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddUnitOfWork().AddService().AddJobService();
            return services;
        }

        public static IServiceCollection AddCommonCoreMvc(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("aspnetcore.common", builder =>
                {
                    builder.WithOrigins("https://localhost:44329")
                    .AllowAnyHeader();
                });
            });

            services.Configure<FormOptions>(x => x.MultipartBodyLengthLimit = 1024 * 1024 * 3);
            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("Default",
                    new CacheProfile
                    {
                        Duration = 3000,
                        Location = ResponseCacheLocation.Any
                    });
                options.CacheProfiles.Add("Never",

                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.None,
                        NoStore = true
                    });
                options.Conventions.Add(new AutoValidateAntiForgeryTokenModelConvention());
                options.Filters.Add(new MvcActionFilter());
            })
            .AddJsonOptions(x => x.SerializerSettings.DateFormatString = "yyyy-HH-dd HH:mm")
            .AddViewLocalization()
            .AddDataAnnotationsLocalization();

            return services;
        }
    }
}
