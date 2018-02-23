using <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories;

namespace <%= projectNamespace %>.Core.Interfaces.DataAccess
{
    public interface IDatabaseService
    {
        ITodoItemsRepository TodoItemsRepository { get; }

        IUsersRepository UsersRepository { get; }

        IRefreshTokensRepository RefreshTokensRepository { get; }

        void MigrateToLatestVersion();

        void SaveChanges();
    }
}