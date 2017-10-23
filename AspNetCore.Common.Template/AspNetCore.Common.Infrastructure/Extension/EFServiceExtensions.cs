
using AspNetCore.Common.Infrastructure.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Common.Infrastructure.Extension
{
    public static class EFServiceExtensions
    {
        public static AppSrvBuilder AddUnitOfWork(this IServiceCollection services) {
            var builder = new AppSrvBuilder(services);
            builder.Services.AddScoped(typeof(IdentityDbContext), typeof(IdentityDbContext));
            //builder.Services.AddScoped(typeof(IRoadDbContext), typeof(RoadDbContext));
            //builder.Services.AddScoped(typeof(IMessageLogDbContext), typeof(MessageLogDbContext));
            return builder;
        }
    }
}