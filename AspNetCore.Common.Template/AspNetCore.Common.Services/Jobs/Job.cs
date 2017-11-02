using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Common.Services.Jobs
{
    public abstract class Job : IJob
    {
        public abstract void Execute();
    }
}
