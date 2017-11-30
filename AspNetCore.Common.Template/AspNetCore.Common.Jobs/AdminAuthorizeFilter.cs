using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;
using Hangfire.Annotations;

namespace AspNetCore.Common.Jobs
{
    public class AdminAuthorizeFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            //var httpcontext = context.GetHttpContext();
            //return httpcontext.User.IsInRole("SysAdmin");
            return true;
        }
    }
}
