using Hangfire;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Common.Jobs
{
    public static class HangfireExtentions
    {
        public static IApplicationBuilder UseHangfire(this IApplicationBuilder app)
        {
            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire",new DashboardOptions() {
                Authorization=new[] {new AdminAuthorizeFilter()}
            });
            return app;
        }

        public static IApplicationBuilder UseHangfireRecurringJobs(this IApplicationBuilder app)
        {
            RecurringJob.AddOrUpdate(() => Console.WriteLine($"ASP.NET Core Hangfire Demo. Current Time is :{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}"), Cron.Minutely());
            return app;
        }

    }
}
