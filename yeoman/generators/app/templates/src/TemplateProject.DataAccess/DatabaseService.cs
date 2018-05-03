using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using <%= projectNamespace %>.Core.Interfaces.DataAccess;
using <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories;
using <%= projectNamespace %>.DataAccess.Repositories;

namespace <%= projectNamespace %>.DataAccess
{
    public sealed class DatabaseService : IDatabaseService
    {
        private readonly ApplicationDbContext _db;

        public DatabaseService(IServiceProvider serviceProvider,
            ApplicationDbContext db)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));
            if (db == null)
                throw new ArgumentNullException(nameof(db));

            _db = db;

            TodoItemsRepository = ActivatorUtilities.CreateInstance<TodoItemsRepository>(serviceProvider, _db);
        }

        public ITodoItemsRepository TodoItemsRepository { get; }

        public void MigrateToLatestVersion()
        {
            _db.Database.Migrate();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}