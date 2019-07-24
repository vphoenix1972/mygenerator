using System.Collections.Generic;
using TemplateProject.Utils.Entities;

namespace TemplateProject.Core.Interfaces.DataAccess.Repositories
{
    public interface IRepositoryBase<TEntity>
        where TEntity : class, IEntity<string>
    {
        (IList<TEntity> Items, int Total) GetMany(string nameFilter = null, int? limit = null, int? skip = null, string sortColumn = null, SortOrder? order = null);

        TEntity GetById(string id);

        TEntity AddOrUpdate(TEntity item);

        void DeleteById(string id);
    }
}