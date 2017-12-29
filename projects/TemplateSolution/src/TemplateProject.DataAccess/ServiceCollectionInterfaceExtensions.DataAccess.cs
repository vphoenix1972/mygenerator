using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TemplateProject.Core.Interfaces.DataAccess;

namespace TemplateProject.DataAccess
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString));
            services.AddScoped<IDatabaseService, DatabaseService>();
        }
    }
}