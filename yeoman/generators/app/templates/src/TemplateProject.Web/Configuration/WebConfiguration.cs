using System;
using System.Globalization;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace <%= projectNamespace %>.Web.Configuration
{
    public sealed class WebConfiguration :
        IWebConfiguration
    {
        private readonly IConfigurationRoot _configurationRoot;

        public WebConfiguration(string configPath)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configPath, optional: true, reloadOnChange: true);

            _configurationRoot = configurationBuilder.Build();
        }

        public string ServerUrls => _configurationRoot["serverUrls"] ?? WebConstants.DefaultServerUrls;

        public string DbConnectionString => _configurationRoot["dbConnectionString"];
    }
}