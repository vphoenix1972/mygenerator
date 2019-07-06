using Microsoft.Extensions.DependencyInjection;
using <%= csprojName %>.Core.Interfaces.DataAccess;

namespace <%= csprojName %>.DataAccess.MongoDB
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddMongoDB(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDatabaseService, DatabaseService>();
        }
    }
}
