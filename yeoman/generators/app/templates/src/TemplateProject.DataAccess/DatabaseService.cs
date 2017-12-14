using <%= projectNamespace %>.Core.Interfaces.DataAccess;
using <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories;
using <%= projectNamespace %>.DataAccess.Repositories;

namespace <%= projectNamespace %>.DataAccess
{
    public sealed class DatabaseService : IDatabaseService
    {
        public DatabaseService()
        {
            TodoItemsRepository = new TodoItemsRepository();
        }

        public ITodoItemsRepository TodoItemsRepository { get; }

        public void SaveChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}