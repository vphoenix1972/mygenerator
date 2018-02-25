using System;

namespace <%= projectNamespace %>.Web.Configuration
{
    public interface IWebConfiguration
    {
        string ServerUrls { get; }

        string DbConnectionString { get; }

        string JwtKey { get; }

        TimeSpan JwtAccessTokenLifetime { get; }
        TimeSpan JwtRefreshTokenLifetime { get; }
        TimeSpan JwtClockSkew { get; }
    }
}