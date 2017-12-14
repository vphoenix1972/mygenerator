using Microsoft.Extensions.DependencyInjection;
using TemplateProject.Core.Interfaces.DataAccess;

namespace TemplateProject.DataAccess
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddDataAccess(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseService, DatabaseService>();
        }
    }
}