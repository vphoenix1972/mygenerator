using <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories;

namespace <%= projectNamespace %>.Core.Interfaces.DataAccess
{
    public interface IDatabaseService
    {
        ITodoItemsRepository TodoItemsRepository { get; }

        void SaveChanges();
    }
}