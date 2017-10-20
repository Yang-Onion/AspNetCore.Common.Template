using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AspNetCore.Common.Infrastructure.Web
{
    [JsonObject]
    public class JsonResponse
    {
        #region ctor

        protected JsonResponse()
        {
        }

        #endregion ctor

        #region public methods

        public static JsonResponse Create()
        {
            return new JsonResponse();
        }

        public static JsonResponse CreateError(string errMsg)
        {
            var jsonResponse = Create();
            jsonResponse.ErrorMessages = errMsg;
            return jsonResponse;
        }

        public static JsonResponse CreateError(Exception ex)
        {
            //JsonResponse res = null;
            //if (ex is FinanceException)
            //{
            //    res = new FinanceJsonResponse();
            //    res.Code = 203;
            //    ((FinanceJsonResponse)res).Money = ((FinanceException)ex).Money;
            //}
            //else
            //{
            //    res = Create();
            //    res.Code = 201;
            //}
            //res.ErrorMessages = ex.Message;
            //if (ex is DomainException)
            //    res.Code = 202;
            //return res;
            return null;
        }

        public static JsonResponse CreateError(List<ValidationErrorItem> errorItem)
        {
            var jsonResponse = Create();
            jsonResponse.Errors = errorItem;
            return jsonResponse;
        }

        public static JsonResponse CreateSystemError(Exception ex)
        {
            var jsonResponse = Create();
            jsonResponse.Errors.Add(new ValidationErrorItem("SystemError", "系统错误"));
            jsonResponse.Errors.Add(new ValidationErrorItem("SystemError", ex.Message));
            var innerEx = ex.InnerException;
            if (innerEx != null)
            {
                jsonResponse.Errors.Add(new ValidationErrorItem("SystemError", innerEx.Message));
                innerEx = innerEx.InnerException;
                if (innerEx != null)
                {
                    jsonResponse.Errors.Add(new ValidationErrorItem("SystemError", innerEx.Message));
                }
            }
            return jsonResponse;
        }

        #endregion public methods

        #region props

        [JsonProperty("isSuccess")]
        public bool IsSuccess
        {
            get
            {
                return Errors.Count == 0 && ErrorMessages == string.Empty;
            }
        }

        [JsonProperty("code")]
        public int Code { get; set; } = 1;

        [JsonProperty("errors")]
        public List<ValidationErrorItem> Errors { get; set; } = new List<ValidationErrorItem>();

        [JsonProperty("errorMessages")]
        public string ErrorMessages { get; set; } = "";

        #endregion props
    }

    [JsonObject]
    public class JsonResponse<T> : JsonResponse
    {
        [JsonProperty("data")]
        public T Data { get; set; }
    }

    public class FinanceJsonResponse : JsonResponse
    {
        [JsonProperty("money")]
        public decimal Money { get; set; }
    }
}