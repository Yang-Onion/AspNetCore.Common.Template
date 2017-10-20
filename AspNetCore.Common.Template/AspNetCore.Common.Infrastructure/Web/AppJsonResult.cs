using System;
using System.Buffers;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using AspNetCore.Common.Infrastructure.Web;

namespace AspNetCore.Common.Infrastructure
{
    public class AppJsonResult : JsonResult
    {
        private static readonly string DefaultContentType =
            new MediaTypeHeaderValue("application/json") { Encoding = Encoding.UTF8 }.ToString();

        public AppJsonResult() : base(null) {
        }

        public AppJsonResult(object value)
            : base(value) {
        }

        public override Task ExecuteResultAsync(ActionContext context) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }
            var response = context.HttpContext.Response;
            JsonResponse data = null;
            var value = ((JsonResult)Value)?.Value;
            if (string.IsNullOrEmpty(value?.ToString())) {
                data = JsonResponse.Create();
            }
            else {
                data = new JsonResponse<object> { Data = value };
            }
            WriteToResponse(response, data);
            //return TaskCache.CompletedTask;
            return Task.CompletedTask;
        }

        public void WriteToResponse(HttpResponse response, JsonResponse data) {
            ResponseContentTypeHelper.ResolveContentTypeAndEncoding(ContentType, response.ContentType,
                DefaultContentType, out string resolvedContentType, out Encoding resolvedContentTypeEncoding);

            response.ContentType = resolvedContentType;

            response.StatusCode = 200;

            var WriterFactory = new HttpResponseStreamWriterFactory();
            using (var writer = WriterFactory.CreateWriter(response.Body, resolvedContentTypeEncoding)) {
                using (var jsonWriter = new JsonTextWriter(writer)) {
                    jsonWriter.ArrayPool = new JsonArrayPool<char>(ArrayPool<char>.Shared);
                    jsonWriter.CloseOutput = false;

                    var jsonSerializer =
                        JsonSerializer.Create(UtilityHelper.JsonSettings());
                    jsonSerializer.Serialize(jsonWriter, data);
                }
            }
        }
    }

    public class HttpResponseStreamWriterFactory : IHttpResponseStreamWriterFactory
    {
        public TextWriter CreateWriter(Stream stream, Encoding encoding) {
            return new HttpResponseStreamWriter(stream, encoding);
        }
    }
}