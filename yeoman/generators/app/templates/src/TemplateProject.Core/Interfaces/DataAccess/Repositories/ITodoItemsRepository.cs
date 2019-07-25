using System.Collections.Generic;
using <%= projectNamespace %>.Core.Domain;
using <%= projectNamespace %>.Utils.Entities;

namespace <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories
{
    public interface ITodoItemsRepository : IRepositoryBase<ITodoItem>
    {
        (IList<ITodoItem> Items, int Total) GetMany(string nameFilter = null, int? limit = null, int? skip = null, string orderBy = null, SortOrder? orderDirection = null);
    }
}