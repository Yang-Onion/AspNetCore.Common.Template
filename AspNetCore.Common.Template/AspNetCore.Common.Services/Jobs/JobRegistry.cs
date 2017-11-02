using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Common.Services.Jobs
{
    public class JobRegistry: Registry
    {
        public JobRegistry()
        {
            //每天0-1点执行
            Schedule<DemoJob>().WithName("DemoJob").ToRunOnceAt(0,1).AndEvery(1).Days();
        }
    }
}
