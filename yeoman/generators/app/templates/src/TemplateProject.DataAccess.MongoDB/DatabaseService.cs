using Microsoft.Extensions.DependencyInjection;
using System;
using <%= csprojName %>.Core.Interfaces.DataAccess;
using <%= csprojName %>.Core.Interfaces.DataAccess.Repositories;
using <%= csprojName %>.DataAccess.MongoDB.TodoItems;

namespace <%= csprojName %>.DataAccess.MongoDB
{
    internal sealed class DatabaseService : IDatabaseService
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            TodoItemsRepository = ActivatorUtilities.CreateInstance<TodoItemsRepository>(serviceProvider);
        }

        public ITodoItemsRepository TodoItemsRepository { get; }

        public void MigrateToLatestVersion()
        {

        }

        public void SaveChanges()
        {

        }
    }
}
