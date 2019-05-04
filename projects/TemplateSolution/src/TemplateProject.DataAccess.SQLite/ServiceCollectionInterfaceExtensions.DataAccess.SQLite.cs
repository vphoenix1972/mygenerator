using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TemplateProject.Core.Interfaces.DataAccess;

namespace TemplateProject.DataAccess.SQLite
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
