using System;

namespace TemplateProject.Web.Configuration
{
    public interface IWebConfiguration
    {
        string DbConnectionString { get; }

        string JwtKey { get; }

        TimeSpan JwtAccessTokenLifetime { get; }
        TimeSpan JwtRefreshTokenLifetime { get; }
        TimeSpan JwtClockSkew { get; }
    }
}