using TemplateProject.Core.Interfaces.DataAccess;
using TemplateProject.Core.Interfaces.DataAccess.Repositories;
using TemplateProject.DataAccess.Repositories;

namespace TemplateProject.DataAccess
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