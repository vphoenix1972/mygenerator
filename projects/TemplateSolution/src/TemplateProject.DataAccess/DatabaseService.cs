using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TemplateProject.Core.Interfaces.DataAccess;
using TemplateProject.Core.Interfaces.DataAccess.Repositories;
using TemplateProject.DataAccess.Repositories;

namespace TemplateProject.DataAccess
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