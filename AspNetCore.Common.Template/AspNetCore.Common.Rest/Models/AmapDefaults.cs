using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Common.Rest.Models
{
    public class AmapDefaults
    {
        public AmapDefaults(IConfigurationSection config)
        {
            BaseUrl = config["BaseUrl"];
            AuthKey = config["AuthKey"];
            OutputFormat = config["OutputFormat"];
            DistanceServiceAddress = config["DistanceServiceAddress"];
        }

        public string AuthKey { get; set; }

        public string OutputFormat { get; set; }

        public string BaseUrl { get; set; }

        public string DistanceServiceAddress { get; set; }

    }
}
