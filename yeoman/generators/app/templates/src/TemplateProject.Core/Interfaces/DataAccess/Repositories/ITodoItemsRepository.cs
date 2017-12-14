using System.Collections.Generic;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories
{
    public interface ITodoItemsRepository
    {
        IList<ITodoItem> GetAll();

        ITodoItem GetById(int id);

        ITodoItem AddOrUpdate(ITodoItem item);

        void DeleteById(int id);
    }
}