using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace <%= csprojName %>.DataAccess.MongoDB
{
    internal sealed class Migrator
    {
        private const string migrationHistoryCollectionName = "__MigrationHistory";

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Migrator> _logger;

        public Migrator(IServiceProvider serviceProvider, ILogger<Migrator> logger)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger;
        }

        public void MigrateToLatestVersion(IMongoDatabase database, List<IMigration> migrations)
        {
            var migrationCollection = database.GetCollection<AppliedMigration>(migrationHistoryCollectionName);

            var appliedMigrations = migrationCollection.Find(FilterDefinition<AppliedMigration>.Empty)
                .SortBy(x => x.Id)
                .ToList();

            if (appliedMigrations.Count > migrations.Count ||
                appliedMigrations.Where((appliedMigration, i) => appliedMigration.Name != GetName(migrations[i])).Any())
                throw new InvalidOperationException("Cannot apply migrations: migration list mismatch");

            for (var i = 0; i < migrations.Count; i++)
            {
                var migration = migrations[i];
                var migrationName = GetName(migration);

                if (i < appliedMigrations.Count)
                {
                    _logger.LogInformation($"Migration '{migrationName}': skip (migration already ran)");
                    continue;
                }
                
                migration.Up(database);

                migrationCollection.InsertOne(new AppliedMigration() {Name = migrationName});
                
                _logger.LogInformation($"Migration '{migrationName}': applied");
            }
        }

        private string GetName(IMigration migration) => migration.GetType().Name;

        private sealed class AppliedMigration
        {
            public ObjectId Id { get; set; }

            public string Name { get; set; }
        }
    }
}