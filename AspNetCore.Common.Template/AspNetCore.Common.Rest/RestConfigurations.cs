using AspNetCore.Common.Rest.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Common.Rest
{
    public class RestConfigurations
    {
        public RestConfigurations(IConfigurationSection thridPartRestSection)
        {
            SMS = new SmsDefaults(thridPartRestSection.GetSection("SMS"));
            Amap = new AmapDefaults(thridPartRestSection.GetSection("Amap"));
        }

        public SmsDefaults SMS { get; set; }
        public AmapDefaults Amap { get; set; }

    }
}
