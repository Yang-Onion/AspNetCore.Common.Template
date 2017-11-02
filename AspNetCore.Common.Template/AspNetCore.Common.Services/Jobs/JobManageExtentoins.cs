using FluentScheduler;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Common.Services.Jobs
{
    public static class JobManageExtentoins
    {
        public static IApplicationBuilder UseJob(this IApplicationBuilder app)
        {
            return app.UseJob(new JobOptions());
        }

        public static IApplicationBuilder UseJob(this IApplicationBuilder app,JobOptions options)
        {
            var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var jobFactory = app.ApplicationServices.GetRequiredService<IJobFactory>();
            var registry = app.ApplicationServices.GetRequiredService<Registry>();
            var logger = loggerFactory.CreateLogger<IJob>();

            JobManager.JobException += e => { logger.LogError(new EventId(1), e.Exception, null, null); };
            JobManager.JobStart += e => { logger.LogInformation("{0} job started at {1}", e.Name, e.StartTime); };
            JobManager.JobEnd += e =>
            {
                logger.LogInformation("{0} job has end. elapsed time:{1}, next run at :{2}", e.Name, e.Duration,
                    e.NextRun);
            };
            JobManager.Initialize(registry);
            return app;
        }
    }
}
