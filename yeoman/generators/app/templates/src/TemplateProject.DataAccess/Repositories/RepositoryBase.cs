using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using <%= projectNamespace %>.Utils.Entities;
using <%= projectNamespace %>.Utils.Factories;

namespace <%= projectNamespace %>.DataAccess.Repositories
{
    public abstract class RepositoryBase<TEntity, TEntityImpl, TKey, TDataModel>
        where TEntity : class, IEntity<TKey?>
        where TEntityImpl: TEntity
        where TKey : struct
        where TDataModel : class, IEntity<TKey>, new()
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<TDataModel> _dbSet;
        private readonly IFactory<TEntityImpl> _entitiesFactory;

        public RepositoryBase(ApplicationDbContext db,
            DbSet<TDataModel> dbSet,
            IFactory<TEntityImpl> entitiesFactory)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            if (dbSet == null)
                throw new ArgumentNullException(nameof(dbSet));
            if (entitiesFactory == null)
                throw new ArgumentNullException(nameof(entitiesFactory));

            _db = db;
            _dbSet = dbSet;
            _entitiesFactory = entitiesFactory;
        }

        protected ApplicationDbContext Db => _db;

        protected DbSet<TDataModel> DbSet => _dbSet;

        protected IFactory<TEntityImpl> EntitiesFactory => _entitiesFactory;

        public IList<TEntity> GetAll()
        {
            return _dbSet.Select(Map).ToList();
        }

        public TEntity GetById(TKey id)
        {
            var item = _dbSet.Find(id);
            if (item == null)
                return null;

            return Map(item);
        }

        public TEntity AddOrUpdate(TEntity item)
        {
            if (item.Id.HasValue)
            {
                var existing = _dbSet.Find(item.Id.Value);
                if (existing != null)
                {
                    Map(item, existing);

                    _dbSet.Update(existing);

                    return item;
                }
            }

            var dataModel = new TDataModel();

            Map(item, dataModel);

            _dbSet.Add(dataModel);

            _db.AddSaveChangesHook(() => item.Id = dataModel.Id);

            return item;
        }

        public void DeleteById(TKey id)
        {
            var existing = _dbSet.Find(id);
            if (existing == null)
                return;

            _dbSet.Remove(existing);
        }

        protected abstract void Map(TDataModel source, TEntityImpl dest);

        protected abstract void Map(TEntity source, TDataModel dest);

        private TEntity Map(TDataModel source)
        {
            var result = _entitiesFactory.Create();

            Map(source, result);

            return result;
        }
    }
}