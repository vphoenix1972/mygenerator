using System.Collections.Generic;
using <%= projectNamespace %>.Utils.Entities;

namespace <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories
{
    public interface IRepositoryBase<TEntity>
        where TEntity : class, IEntity<string>
    {
        IList<TEntity> GetAll();

        TEntity GetById(string id);

        TEntity AddOrUpdate(TEntity item);

        void DeleteById(string id);
    }
}