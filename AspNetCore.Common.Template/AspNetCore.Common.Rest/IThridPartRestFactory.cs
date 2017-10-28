using AspNetCore.Common.Rest.Providers;
using Microsoft.Extensions.Configuration;

namespace AspNetCore.Common.Rest
{
    public interface IThridPartRestFactory
    {
        IConfiguration Configuration { get; set; }

        RestConfigurations RestConfiguration { get; set; }

        SmsProvider SmsProvider { get; }
        AmapProvider AmapProvider { get; }
    }
}
