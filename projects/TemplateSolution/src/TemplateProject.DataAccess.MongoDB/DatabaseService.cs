using Microsoft.Extensions.DependencyInjection;
using System;
using TemplateProject.Core.Interfaces.DataAccess;
using TemplateProject.Core.Interfaces.DataAccess.Repositories;
using TemplateProject.DataAccess.MongoDB.TodoItems;

namespace TemplateProject.DataAccess.MongoDB
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
