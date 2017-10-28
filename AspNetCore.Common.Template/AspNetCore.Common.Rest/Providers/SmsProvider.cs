using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AspNetCore.Common.Rest.Providers
{
    public class SmsProvider
    {
        private static readonly HttpClient _client = new HttpClient();
        private readonly RestConfigurations _config;
        private readonly ILogger _logger;
        private string _queryParams;

        public SmsProvider(RestConfigurations config)
        {
            _config = config;
            BaseUri = new Uri(config.SMS.Address);
            if (_client.BaseAddress==null)
            {
                _client.BaseAddress = new Uri(config.SMS.Address);
            }
            _queryParams = $"uid={config.SMS.UserName}&pwd={config.SMS.Password}";
        }
        public SmsProvider(RestConfigurations config,ILogger logger):this(config)
        {
            _logger = logger;
        }

        public Uri BaseUri { get; set; }


        public async Task<string> SendAsync(string phoneNumber,string content)
        {
            var encodeContent = WebUtility.UrlEncode(content);
            _queryParams += $"&otime=&tos={phoneNumber}&msg={encodeContent}";

            var url = $"/service.asmx/SendMessages?{_queryParams}";

            var result = await _client.GetStringAsync(url);

            XElement element = null;
            try
            {
                element = XElement.Parse(result);

                return element.FirstNode.ToString();
            }
            catch
            {
                _logger.LogError(result, url);
                return result;
            }
        }
    }
}
