using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using TemplateProject.Core.Interfaces.DataAccess;
using TemplateProject.Core.Interfaces.DataAccess.Repositories;
using TemplateProject.DataAccess.MongoDB.TodoItems;

namespace TemplateProject.DataAccess.MongoDB
{
    internal sealed class DatabaseService : IDatabaseService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Lazy<TodoItemsRepository> _todoItemsRepository;

        public DatabaseService(IServiceProvider serviceProvider, string database)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            _todoItemsRepository = new Lazy<TodoItemsRepository>(
                () => ActivatorUtilities.CreateInstance<TodoItemsRepository>(
                    _serviceProvider,
                    _serviceProvider.GetRequiredService<MongoClient>().GetDatabase(database)
            ));
        }

        public ITodoItemsRepository TodoItemsRepository => _todoItemsRepository.Value;

        public void MigrateToLatestVersion()
        {

        }

        public void SaveChanges()
        {
            // Not used - transactions only supported in replica set mongos
        }
    }
}
