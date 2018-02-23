using System;

namespace <%= projectNamespace %>.Web.Security
{
    public static class WebSecurityConstants
    {
        public const string JwtIssuer = "<%= jwtIssuer %>";
        public const string JwtAudience = "<%= jwtAudience %>";

        public static readonly TimeSpan DefaultJwtAccessTokenLifetime = TimeSpan.FromMinutes(30);
        public static readonly TimeSpan DefaultJwtRefreshTokenLifetime = TimeSpan.FromHours(24);
        public static readonly TimeSpan DefaultJwtClockSkew = TimeSpan.FromMinutes(5);

        public const string AccessTokenUserIdKey = "userId";

        public const string RefreshTokenTokenIdKey = "tokenId";
    }
}