using AspNetCore.Common.Infrastructure.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Common.Rest.Providers
{
    public class AmapProvider
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly RestConfigurations _config;
        private readonly ILogger _logger;
        public AmapProvider(RestConfigurations config)
        {
            _config = config;
            BaseUri = new Uri(config.Amap.BaseUrl);
            if (_httpClient.BaseAddress==null)
            {
                _httpClient.BaseAddress= new Uri(config.Amap.BaseUrl);
            }
        }
        public AmapProvider(RestConfigurations config,ILogger logger) : this(config)
        {
            _logger = logger;
        }

        public Uri BaseUri { get; set; }

        /// <summary>
        /// 获取城市间距离
        /// </summary>
        /// <param name="fromCityId"></param>
        /// <param name="toCityId"></param>
        /// <param name="locationResolver">根据城市id解析location</param>
        /// <param name="mileResolver">从持久化集合中获取城市间距离(from,to)</param>
        /// <param name="mileStore">持久化地图获取的距离(fromid,fromlocation,toid,tolocation,distance)</param>
        /// <returns></returns>
        public async Task<int> GetDistance(string fromCityId, string toCityId,
            Func<string, string> locationResolver,
            Func<string, string, int?> mileResolver,
            Action<string, string, string, string, int> mileStore)
        {
            if (string.Equals(fromCityId, toCityId)) return 0;
            var dbDistance = mileResolver(fromCityId, toCityId);
            if (dbDistance.HasValue) return dbDistance.Value;
            var fromCityLocation = locationResolver(fromCityId);
            var toCityLocation = locationResolver(toCityId);
            var distance = 0;
            var url = BuildUri(_config.Amap.DistanceServiceAddress,
                new Dictionary<string, string>
                {
                    {"key", _config.Amap.AuthKey},
                    {"output", _config.Amap.OutputFormat},
                    {"origins", fromCityLocation},
                    {"destination", toCityLocation}
                });
            try
            {
                var res = await _httpClient.GetAsync(url);

                var resJson = JObject.Parse(await res.Content.ReadAsStringAsync());
                distance = resJson["results"].Select(xx => { return xx["distance"].Value<int>(); }).FirstOrDefault();
                _logger.LogInformation(string.Format("Amap get distance:{0}, result:{1}", url, distance));
            }
            catch
            {
                throw new DomainException("获取城市间距离失败,请稍后再试");
            }
            try
            {
                mileStore(fromCityId, fromCityLocation, toCityId, toCityLocation, distance);
            }
            catch (Exception e)
            {
                if (!(e is DomainException))
                    throw new DomainException("获取城市间距离失败,请稍后再试");
            }

            return distance;
        }

        private string BuildUri(string addr, IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            var uri = new FormUrlEncodedContent(nameValueCollection);
            var address = uri.ReadAsStringAsync().Result;
            var tokenAdd = new Uri(BaseUri, addr);
            var builder = new UriBuilder(tokenAdd)
            {
                Query = address
            };
            return builder.ToString();
        }

    }
}
