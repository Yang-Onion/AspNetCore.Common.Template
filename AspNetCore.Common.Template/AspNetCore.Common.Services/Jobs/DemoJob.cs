using AspNetCore.Common.Infrastructure.Data;
using AspNetCore.Common.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Common.Services.Jobs
{
    public class DemoJob : Job
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _provider;
        public DemoJob(IServiceProvider serviceProvider,ILoggerFactory loggerFactory)
        {
            _provider = serviceProvider;
            _logger = loggerFactory.CreateLogger(typeof(DemoJob));
        }
        public override void Execute()
        {
            using (var scope = _provider.CreateScope())
            {
                var provider = scope.ServiceProvider;
                //同样可以拿到其他接口的实例
                var dbcontext = provider.GetRequiredService<IdentityDbContext>();

                dbcontext.Menus.Add(new Models.Identity.Menu
                {

                });
                
            }
        }
    }
}
