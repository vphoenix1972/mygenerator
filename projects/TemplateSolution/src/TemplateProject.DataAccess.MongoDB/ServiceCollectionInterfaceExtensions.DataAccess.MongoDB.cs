using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TemplateProject.Core.Interfaces.DataAccess;

namespace TemplateProject.DataAccess.MongoDB
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddMongoDB(this IServiceCollection services, string connectionString)
        {
            var database = new MongoUrl(connectionString).DatabaseName;

            services.AddSingleton(_ => new MongoClient(connectionString));
            services.AddScoped<IDatabaseService, DatabaseService>(sp => ActivatorUtilities.CreateInstance<DatabaseService>(sp, database));            
        }
    }
}
