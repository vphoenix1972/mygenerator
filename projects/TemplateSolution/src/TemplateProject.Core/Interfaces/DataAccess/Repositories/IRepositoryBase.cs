using TemplateProject.Utils.Entities;

namespace TemplateProject.Core.Interfaces.DataAccess.Repositories
{
    public interface IRepositoryBase<TEntity>
        where TEntity : class, IEntity<string>
    {
        TEntity GetById(string id);

        TEntity AddOrUpdate(TEntity item);

        void DeleteById(string id);
    }
}