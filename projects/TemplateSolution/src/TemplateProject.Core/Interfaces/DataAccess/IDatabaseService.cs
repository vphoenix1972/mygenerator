using TemplateProject.Core.Interfaces.DataAccess.Repositories;

namespace TemplateProject.Core.Interfaces.DataAccess
{
    public interface IDatabaseService
    {
        ITodoItemsRepository TodoItemsRepository { get; }

        void SaveChanges();
    }
}