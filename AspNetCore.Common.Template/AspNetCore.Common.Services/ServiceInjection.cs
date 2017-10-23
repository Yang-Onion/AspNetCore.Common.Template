using AspNetCore.Common.Infrastructure.Core;
using AspNetCore.Common.Models.Common;
using AspNetCore.Common.Services.Identity.Impl;
using AspNetCore.Common.Services.Impl;
using AspNetCore.Common.Services.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Common.Services
{
    public static class ServiceInjection
    {

        public static AppSrvBuilder AddService(this AppSrvBuilder builder)
        {
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });

            builder.AddSingleton(mapper.CreateMapper());
            //builder.AddScoped<IViewRenderService, ViewRenderService>();
            //builder.AddTransient<IEmailSender, AuthMessageSender>();
            builder.AddTransient<ISmsSender, SmsSender>();
            builder.AddScoped(typeof(MenuManager));
            builder.AddScoped(typeof(AppUserManager));
            builder.AddScoped(typeof(AppRoleManager));
            builder.AddScoped(typeof(SignInManager));

            builder.AddScoped(typeof(IMenu), typeof(MenuService));
            //builder.AddTransient<IThirdPartyRestProvider, ThirdPartyRestProvider>();

            //AddRoadService(builder);

            return builder;
        }
    }
}
