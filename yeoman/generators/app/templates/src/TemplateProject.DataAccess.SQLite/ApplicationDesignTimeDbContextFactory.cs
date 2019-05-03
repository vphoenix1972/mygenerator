using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace <%= projectNamespace %>.DataAccess.SQLite
{
    internal sealed class ApplicationDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private const string fileWithConnectionStringForMigrations = "connectionStringForMigrations";

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Hack
            // Set connection string to developer's database to work with migrations
            // in fileWithConnectionStringForMigrations file
            var connectionString = File.ReadAllText(fileWithConnectionStringForMigrations);

            var builderOption = new DbContextOptionsBuilder<ApplicationDbContext>();

            builderOption.UseSqlite(connectionString);

            return new ApplicationDbContext(builderOption.Options);
        }
    }
}