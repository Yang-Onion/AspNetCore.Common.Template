using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore.Common.Web.Providers
{
    public class ResponseExpiresAttribute : ActionFilterAttribute
    {
        private readonly TimeSpan _duration;

        public ResponseExpiresAttribute(int seconds)
        {
            _duration = new TimeSpan(0, 0, seconds);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add("Expires", DateTime.Now.Add(_duration).ToString("R"));
            base.OnActionExecuted(context);
        }
    }
}