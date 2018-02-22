using Microsoft.Extensions.DependencyInjection;
using TemplateProject.Utils.Md5;

namespace TemplateProject.Utils
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddUtils(this IServiceCollection services)
        {
            services.AddTransient<IMd5Crypter, Md5Crypter>();
        }
    }
}