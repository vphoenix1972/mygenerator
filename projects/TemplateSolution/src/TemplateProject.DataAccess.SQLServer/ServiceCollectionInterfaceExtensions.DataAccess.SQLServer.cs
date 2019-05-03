using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TemplateProject.Core.Interfaces.DataAccess;

namespace TemplateProject.DataAccess.SQLServer
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddSQLServer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IDatabaseService, DatabaseService>();
        }
    }
}
