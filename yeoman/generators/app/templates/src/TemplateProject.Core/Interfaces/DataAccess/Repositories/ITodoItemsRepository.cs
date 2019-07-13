using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories
{
    public interface ITodoItemsRepository : IRepositoryBase<ITodoItem>
    {
    }
}