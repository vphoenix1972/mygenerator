using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using <%= projectNamespace %>.Core.Interfaces.DataAccess;

namespace <%= projectNamespace %>.DataAccess.SQLServer
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
