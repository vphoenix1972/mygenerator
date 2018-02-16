using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TemplateProject.Web
{
    public static class WebProjectConstants
    {
        public const string NLogConfigPath = "NLog.config";

        public const string RoleAdmin = "admin";
        public const string RoleUser = "user";

        public const string JwtIssuer = "TemplateProject";
        public const string JwtAudience = "TemplateProject";
        private const string jwtKey = "8ff39b5d-de3b-4bef-9313-dd5329b03689";
        public static readonly TimeSpan JwtAccessTokenLifetime = TimeSpan.FromMinutes(10);
        public static readonly TimeSpan JwtRefreshTokenLifetime = TimeSpan.FromSeconds(15);
        public static readonly TimeSpan JwtClockSkew = TimeSpan.FromMinutes(5);

        public static SymmetricSecurityKey GetJwtSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey));
        }
    }
}