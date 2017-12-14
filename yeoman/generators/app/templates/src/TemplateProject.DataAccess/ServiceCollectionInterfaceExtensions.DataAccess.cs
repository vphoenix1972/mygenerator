using Microsoft.Extensions.DependencyInjection;
using <%= projectNamespace %>.Core.Interfaces.DataAccess;

namespace <%= projectNamespace %>.DataAccess
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddDataAccess(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseService, DatabaseService>();
        }
    }
}