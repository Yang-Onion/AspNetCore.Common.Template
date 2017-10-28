using System;
using System.Collections.Generic;
using System.Text;
using AspNetCore.Common.Rest.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AspNetCore.Common.Rest
{
    public class ThirdPartRestFactory : IThridPartRestFactory
    {
        private readonly RestConfigurations _config;
        private readonly ILogger _logger;
        public ThirdPartRestFactory(IConfiguration config, ILoggerFactory loggerFactory)
        {
            var restSection = config.GetSection("");
            Configuration = config;
            _config = RestConfiguration = new RestConfigurations(restSection);

            _logger = loggerFactory.CreateLogger<ThirdPartRestFactory>();


        }


        public IConfiguration Configuration { get; set; }
        public RestConfigurations RestConfiguration { get; set; }

        public SmsProvider SmsProvider => new SmsProvider(_config, _logger);

        public AmapProvider AmapProvider => new AmapProvider(_config, _logger);
    }
}
