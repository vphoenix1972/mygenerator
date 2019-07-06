using Microsoft.Extensions.DependencyInjection;
using TemplateProject.Core.Interfaces.DataAccess;

namespace TemplateProject.DataAccess.MongoDB
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddMongoDB(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDatabaseService, DatabaseService>();
        }
    }
}
