using Microsoft.Extensions.DependencyInjection;
using <%= projectNamespace %>.Utils.Md5;

namespace <%= projectNamespace %>.Utils
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddUtils(this IServiceCollection services)
        {
            services.AddTransient<IMd5Crypter, Md5Crypter>();
        }
    }
}