using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Common.Rest.Models
{
    public class SmsDefaults
    {
        public SmsDefaults(IConfigurationSection config)
        {
            Address = config["Address"];
            UserName = config["UserName"];
            Password = config["Password"];
        }

        public string Address { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
