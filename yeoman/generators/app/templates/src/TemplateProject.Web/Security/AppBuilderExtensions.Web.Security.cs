using Microsoft.AspNetCore.Builder;

namespace <%= projectNamespace %>.Web.Security
{
    public static class SecurityAppBuilderExtensions
    {
        public static void UseSecurity(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}