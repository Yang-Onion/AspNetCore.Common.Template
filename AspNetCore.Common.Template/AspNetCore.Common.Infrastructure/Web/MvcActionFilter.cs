using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Cms.Infrastructure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using AspNetCore.Common.Infrastructure.Core;

namespace AspNetCore.Common.Infrastructure.Web
{
    public class MvcActionFilter : ActionFilterAttribute, IExceptionFilter
    {
        private Stopwatch _usedTime = new Stopwatch();
        private readonly ILoggerFactory loggerFactory;
        private ILogger _logger;

        public void OnException(ExceptionContext context)
        {
            var logFactory = context.HttpContext.RequestServices.GetService<ILoggerFactory>();
            _logger = logFactory.CreateLogger<MvcActionFilter>();

            //财务回调api请求
            var isFinanceApiCall = context.RouteData.Values.Any(g => string.Equals(g.Key, "fromApi", System.StringComparison.CurrentCultureIgnoreCase) && string.Equals(g.Value.ToString(), "finance", System.StringComparison.CurrentCultureIgnoreCase));
            if (context.Exception is DomainException)
            {
                _logger.LogInformation(string.Format("调用【" + context.ActionDescriptor.DisplayName + "】发生了业务异常:{0}", context.Exception.Message));

                if (isFinanceApiCall)
                    context.Result = ApiJsonResult.Fail(context.Exception.Message);
                else if (context.HttpContext.Request.IsAjaxRequest())
                    context.Result = new JsonResult(JsonResponse.CreateError(context.Exception));
            }
            else
            {
                _logger.LogError(new EventId(1), context.Exception, string.Format("调用" + context.ActionDescriptor.DisplayName + "出错:{0}\r\n{1}", context.Exception.Message, context.Exception.StackTrace));
                if (isFinanceApiCall)
                    context.Result = ApiJsonResult.Fail("系统错误");
                else if (context.HttpContext.Request.IsAjaxRequest())
                    context.Result = new JsonResult(JsonResponse.CreateError("系统错误"));
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _usedTime.Restart();
            //未通过参数验证
            if (!context.ModelState.IsValid)
            {
                var errors = new List<ValidationErrorItem>();
                foreach (KeyValuePair<string, ModelStateEntry> modelState in context.ModelState)
                {
                    var valueErrorMsgs = modelState.Value.Errors;
                    var message = string.Empty;
                    foreach (var msg in valueErrorMsgs)
                    {
                        message += msg.ErrorMessage + ":" + msg.Exception?.Message + ";";
                    }
                    message = message.TrimEnd(',');
                    if (!string.IsNullOrEmpty(message))
                    {
                        var errorItem = new ValidationErrorItem(modelState.Key, message);
                        errors.Add(errorItem);
                    }
                }
                if (context.HttpContext.Request.IsAjaxRequest())
                    context.Result = new JsonResult(JsonResponse.CreateError(errors));
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is JsonResult && !(context.Result is ApiJsonResult))
            {
                context.Result = new AppJsonResult((JsonResult)context.Result);
            }
            else
            {
                base.OnActionExecuted(context);
            }
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            _usedTime.Stop();

            var logFactory = context.HttpContext.RequestServices.GetService<ILoggerFactory>();
            _logger = logFactory.CreateLogger<MvcActionFilter>();
            var actionName = context.ActionDescriptor.DisplayName;
            if (_usedTime.ElapsedMilliseconds > 3000)
            {
                _logger.LogWarning($"Action：{actionName}，执行用时：{_usedTime.ElapsedMilliseconds} ms");
            }

            if (context.Exception != null && context.Exception is SessionTimeoutException)
            {
                context.ExceptionHandled = true;
                context.HttpContext.Response.Redirect((string)context.HttpContext.Request.PathBase +
                                                      "/Account/Login?returnUrl=" + context.HttpContext.Request.Path);
            }
            else
            {
                base.OnResultExecuted(context);
            }
        }
    }
}