using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using <%= projectNamespace %>.Core.Interfaces.DataAccess;

namespace <%= projectNamespace %>.DataAccess
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString));

            /*
             * Uncomment the following line if you want to use SQLite instead of PostgreSQL.
             * Don't forget to change connection string for SQLite in appsettings.json
             * (example: "dbConnectionString": "Data Source=app.db").
             *
             * Note: SQLite support is not implemented in docker build, so if you want to use docker, choose PostgreSQL.
             *
            */

            // services.AddDbContext<ApplicationDbContext>(o => o.UseSqlite(connectionString));


            services.AddScoped<IDatabaseService, DatabaseService>();
        }
    }
}