using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using <%= projectNamespace %>.Web.Configuration;

namespace <%= projectNamespace %>.Web.Security
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddWebSecurity(this IServiceCollection services, IWebConfiguration config)
        {
            var securityService = new WebSecurityService(config);

            services.AddTransient<IWebSecurityService, WebSecurityService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = securityService.JwtTokenValidationParameters;
                });
        }
    }
}