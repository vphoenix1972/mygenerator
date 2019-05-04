using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using <%= projectNamespace %>.Core.Interfaces.DataAccess;

namespace <%= projectNamespace %>.DataAccess.PostgreSQL
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddPostgreSQL(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString));

            services.AddScoped<IDatabaseService, DatabaseService>();
        }
    }
}
