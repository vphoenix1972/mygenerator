using System;

namespace TemplateProject.Web.Security
{
    public static class WebSecurityConstants
    {
        public const string JwtIssuer = "TemplateProject";
        public const string JwtAudience = "TemplateProject";

        public static readonly TimeSpan DefaultJwtAccessTokenLifetime = TimeSpan.FromMinutes(30);
        public static readonly TimeSpan DefaultJwtRefreshTokenLifetime = TimeSpan.FromHours(24);
        public static readonly TimeSpan DefaultJwtClockSkew = TimeSpan.FromMinutes(5);

        public const string RefreshTokenTokenIdKey = "token_id";
    }
}