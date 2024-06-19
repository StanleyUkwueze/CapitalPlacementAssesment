using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitaPlacementUnitTest
{
    public static class TestConfigurationProvider
    {
        public static IConfiguration GetConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return configBuilder.Build();
        }
    }
}
