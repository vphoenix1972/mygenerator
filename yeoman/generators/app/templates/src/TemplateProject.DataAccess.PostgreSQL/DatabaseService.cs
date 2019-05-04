using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using <%= projectNamespace %>.Core.Interfaces.DataAccess;
using <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories;
using <%= projectNamespace %>.DataAccess.PostgreSQL.RefreshTokens;
using <%= projectNamespace %>.DataAccess.PostgreSQL.TodoItems;
using <%= projectNamespace %>.DataAccess.PostgreSQL.Users;

namespace <%= projectNamespace %>.DataAccess.PostgreSQL
{
    internal sealed class DatabaseService : IDatabaseService
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
            UsersRepository = ActivatorUtilities.CreateInstance<UsersRepository>(serviceProvider, _db);
            RefreshTokensRepository = ActivatorUtilities.CreateInstance<RefreshTokensRepository>(serviceProvider, _db);
        }

        public ITodoItemsRepository TodoItemsRepository { get; }

        public IUsersRepository UsersRepository { get; }

        public IRefreshTokensRepository RefreshTokensRepository { get; }

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
