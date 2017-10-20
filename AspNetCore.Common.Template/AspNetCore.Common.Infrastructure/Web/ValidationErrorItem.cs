using Newtonsoft.Json;

namespace AspNetCore.Common.Infrastructure.Web
{
    [JsonObject]
    public class ValidationErrorItem
    {
        public ValidationErrorItem(string propertyName, string errorMessage) {
            ParamKey = propertyName;
            ErrorMessage = errorMessage;
        }

        [JsonProperty("propertyName")]
        public string ParamKey { get; private set; } = "";

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; private set; } = "";
    }
}