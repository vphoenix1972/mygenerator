using System.Collections.Generic;
using TemplateProject.Utils.Entities;

namespace TemplateProject.Core.Interfaces.DataAccess.Repositories
{
    public interface IRepositoryBase<TEntity, in TKey>
        where TEntity : class, IEntity<TKey?>
        where TKey : struct
    {
        IList<TEntity> GetAll();

        TEntity GetById(TKey id);

        TEntity AddOrUpdate(TEntity item);

        void DeleteById(TKey id);
    }
}