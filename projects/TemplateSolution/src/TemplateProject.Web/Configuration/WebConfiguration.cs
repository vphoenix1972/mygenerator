using System;
using System.Globalization;
using System.IO;
using Microsoft.Extensions.Configuration;
using TemplateProject.Web.Security;

namespace TemplateProject.Web.Configuration
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

        public string DbConnectionString => _configurationRoot["dbConnectionString"];

        public string JwtKey => _configurationRoot["jwtKey"];

        public TimeSpan JwtAccessTokenLifetime => GetOrDefault("jwtAccessTokenLifetime",
            WebSecurityConstants.DefaultJwtAccessTokenLifetime);

        public TimeSpan JwtRefreshTokenLifetime => GetOrDefault("jwtRefreshTokenLifetime",
            WebSecurityConstants.DefaultJwtRefreshTokenLifetime);

        public TimeSpan JwtClockSkew => GetOrDefault("jwtClockSkew",
            WebSecurityConstants.DefaultJwtClockSkew);

        private TimeSpan GetOrDefault(string key, TimeSpan defaultValue)
        {
            var valueStr = _configurationRoot[key];
            if (string.IsNullOrWhiteSpace(valueStr))
                return defaultValue;

            TimeSpan result;
            if (!TimeSpan.TryParse(valueStr, CultureInfo.InvariantCulture, out result))
                return defaultValue;

            return result;
        }
    }
}