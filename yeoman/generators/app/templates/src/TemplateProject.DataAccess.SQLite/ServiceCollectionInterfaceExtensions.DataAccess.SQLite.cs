using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using <%= projectNamespace %>.Core.Interfaces.DataAccess;

namespace <%= projectNamespace %>.DataAccess.SQLite
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddSQLite(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlite(connectionString));

            services.AddScoped<IDatabaseService, DatabaseService>();
        }
    }
}
