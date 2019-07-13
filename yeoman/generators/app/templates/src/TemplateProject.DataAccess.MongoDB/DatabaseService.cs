using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using <%= csprojName %>.Core.Interfaces.DataAccess;
using <%= csprojName %>.Core.Interfaces.DataAccess.Repositories;
using <%= csprojName %>.DataAccess.MongoDB.TodoItems;

namespace <%= csprojName %>.DataAccess.MongoDB
{
    internal sealed class DatabaseService : IDatabaseService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _database;
        private readonly Lazy<TodoItemsRepository> _todoItemsRepository;

        public DatabaseService(IServiceProvider serviceProvider, string database)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _database = database ?? throw new ArgumentNullException(nameof(database));

            _todoItemsRepository = new Lazy<TodoItemsRepository>(
                () => ActivatorUtilities.CreateInstance<TodoItemsRepository>(
                    _serviceProvider,
                    GetDatabase()
            ));
        }

        public ITodoItemsRepository TodoItemsRepository => _todoItemsRepository.Value;

        public IUsersRepository UsersRepository => throw new NotImplementedException();

        public IRefreshTokensRepository RefreshTokensRepository => throw new NotImplementedException();

        public void MigrateToLatestVersion()
        {
            var migrator = _serviceProvider.GetRequiredService<Migrator>();

            migrator.MigrateToLatestVersion(GetDatabase(), GetMigrations());
        }

        public void SaveChanges()
        {
            // Not used - transactions only supported in replica set mongos
        }

        private IMongoDatabase GetDatabase() =>
            _serviceProvider.GetRequiredService<MongoClient>().GetDatabase(_database);

        // Add mongodb migrations here
        private List<IMigration> GetMigrations() =>
            new List<IMigration>
            {
                new Migrations.Initial()
            };
    }
}
