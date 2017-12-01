using System;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProtoBuf;
using AspNetCore.Common.Models.Identity;

namespace AspNetCore.Common.Infrastructure.Web
{
    public static class WebExtensions
    {
        public static T Get<T>(this ISession session, string key) where T : class {
            if (session.TryGetValue(key, out byte[] byteArray))
            {
                if (byteArray.Length == 0)
                {
                    return default(T);
                }
                using (var memoryStream = new MemoryStream(byteArray))
                {
                    var obj = Serializer.Deserialize<T>(memoryStream);
                    return obj;
                }
            }
            return null;
        }

        public static void Set<T>(this ISession session, string key, T value) where T : class {
            try {
                using (var memoryStream = new MemoryStream()) {
                    Serializer.Serialize(memoryStream, value);
                    byte[] byteArray = memoryStream.ToArray();
                    session.Set(key, byteArray);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public static bool IsAjaxRequest(this HttpRequest request) {
            return string.Equals(request.Query["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal) ||
                   string.Equals(request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal) ||
                    request.Path.StartsWithSegments(new PathString("/api"));
        }

        public static string Scheme(this HttpRequest request) {
            var _env = (IHostingEnvironment)request.HttpContext.RequestServices.GetService(typeof(IHostingEnvironment));
            if (_env == null || _env.IsDevelopment()) {
                return "http";
            }
            return "https";
        }

        public static PathString CurrentPath(this RazorPage page) {
            var actionDescription = page.ViewContext.ActionDescriptor;
            var resultPath = string.Empty;
            var routeAttribute = actionDescription.AttributeRouteInfo?.Template;
            if (routeAttribute != null) {
                resultPath = string.Concat(Path.AltDirectorySeparatorChar, routeAttribute);
            }
            else {
                var area = actionDescription.RouteValues["area"];
                if (!string.IsNullOrEmpty(area)) {
                    area = string.Concat(Path.AltDirectorySeparatorChar, area);
                }
                var controller = actionDescription.RouteValues["controller"];
                var action = actionDescription.RouteValues["action"];
                resultPath = string.Concat(area, Path.AltDirectorySeparatorChar, controller, Path.AltDirectorySeparatorChar, action);
            }
            return new PathString(resultPath);
        }

        public static string GetUserId(this ClaimsPrincipal user) {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }


        public static AppUser AsAppUser(this ClaimsPrincipal user) {
            if (user == null || user.Identity.Name == null) {
                return null;
            }
            var uid = GetUserId(user);
            var uname = user.FindFirst(ClaimTypes.Name).Value;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var phone = user.FindFirst(ClaimTypes.MobilePhone).Value;
            return new AppUser() { Id = uid, UserName = uname, Email = email, PhoneNumber = phone };
        }

        public static void LogInformation(this ILogger logger, string message, object arg) {
            logger.LogInformation(string.Format(message, JsonConvert.SerializeObject(arg, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
        }
    }
}