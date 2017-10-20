using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Common.Infrastructure
{
    /// <summary>
    /// 系统间接口返回类型
    /// </summary>
    public class ApiJsonResult : JsonResult
    {
        public ApiJsonResult(object value) : base(value) {
        }

        public static ApiJsonResult Success() {
            return new ApiJsonResult(new { code = 1, result = "success", msg = "" });
        }

        public static ApiJsonResult Fail(string msg) {
            return new ApiJsonResult(new { code = 0, result = "fail", msg = msg });
        }
    }
}