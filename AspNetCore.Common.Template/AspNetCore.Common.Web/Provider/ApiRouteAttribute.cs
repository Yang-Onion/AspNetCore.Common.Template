using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore.Common.Web.Providers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ApiRouteAttribute : ActionFilterAttribute
    {
        public string ApiFrom { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.RouteData.Values.Add("fromApi", ApiFrom);
            base.OnActionExecuting(context);
        }
    }
}