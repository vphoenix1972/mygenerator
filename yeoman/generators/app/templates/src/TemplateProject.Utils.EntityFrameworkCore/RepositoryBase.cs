using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using <%= projectNamespace %>.Utils.Entities;
using <%= projectNamespace %>.Utils.Factories;

namespace <%= projectNamespace %>.Utils.EntityFrameworkCore
{
    public abstract class RepositoryBase<TEntity, TEntityImpl, TDataModel, TKey, TDataModelKey>
        where TEntity : class, IEntity<TKey>
        where TEntityImpl : TEntity
        where TDataModel : class, IEntity<TDataModelKey>, new()
    {
        private readonly DbContextWithSaveChangesHooks _db;
        private readonly IMapper _mapper;
        private readonly DbSet<TDataModel> _dbSet;
        private readonly IFactory<TEntityImpl> _entitiesFactory;

        public RepositoryBase(DbContextWithSaveChangesHooks db,
            IMapper mapper,
            DbSet<TDataModel> dbSet,
            IFactory<TEntityImpl> entitiesFactory)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
            _entitiesFactory = entitiesFactory ?? throw new ArgumentNullException(nameof(entitiesFactory));
        }

        protected DbContextWithSaveChangesHooks Db => _db;

        protected DbSet<TDataModel> DbSet => _dbSet;

        protected IFactory<TEntityImpl> EntitiesFactory => _entitiesFactory;

        public IList<TEntity> GetAll()
        {
            return _dbSet.Select(Map).ToList();
        }

        public TEntity GetById(TKey id)
        {
            var item = _dbSet.Find(MapKey(id));
            if (item == null)
                return null;

            return Map(item);
        }

        public TEntity AddOrUpdate(TEntity item)
        {
            if (!IsSet(item.Id))
            {
                var existing = _dbSet.Find(MapKey(item.Id));
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

            _db.AddSaveChangesHook(() => item.Id = MapKey(dataModel.Id));

            return item;
        }

        public void DeleteById(TKey id)
        {
            var existing = _dbSet.Find(MapKey(id));
            if (existing == null)
                return;

            _dbSet.Remove(existing);
        }

        protected virtual void Map(TDataModel source, TEntityImpl dest)
        {
            _mapper.Map(source, dest);
        }

        protected virtual void Map(TEntity source, TDataModel dest)
        {
            _mapper.Map(source, dest);
        }

        protected TEntity Map(TDataModel source)
        {
            var result = _entitiesFactory.Create();

            Map(source, result);

            return result;
        }

        protected abstract bool IsSet(TKey id);
        protected abstract TDataModelKey MapKey(TKey id);
        protected abstract TKey MapKey(TDataModelKey id);
    }

    public abstract class RepositoryBase<TEntity, TEntityImpl, TDataModel> :
        RepositoryBase<TEntity, TEntityImpl, TDataModel, string, int>
        where TEntity : class, IEntity<string>
        where TEntityImpl : TEntity
        where TDataModel : class, IEntity<int>, new()
    {
        public RepositoryBase(DbContextWithSaveChangesHooks db,
            IMapper mapper,
            DbSet<TDataModel> dbSet,
            IFactory<TEntityImpl> entitiesFactory) :
            base(db, mapper, dbSet, entitiesFactory)
        {

        }

        protected override bool IsSet(string id) => id ==  null;
        protected override int MapKey(string id) => int.Parse(id, CultureInfo.InvariantCulture);
        protected override string MapKey(int id) => id.ToString();
    }

    public abstract class RepositoryKeyIntBase<TEntity, TEntityImpl, TDataModel> :
        RepositoryBase<TEntity, TEntityImpl, TDataModel, int?, int>
        where TEntity : class, IEntity<int?>
        where TEntityImpl : TEntity
        where TDataModel : class, IEntity<int>, new()
    {
        public RepositoryKeyIntBase(DbContextWithSaveChangesHooks db,
            IMapper mapper,
            DbSet<TDataModel> dbSet,
            IFactory<TEntityImpl> entitiesFactory) :
            base(db, mapper, dbSet, entitiesFactory)
        {

        }

        protected override bool IsSet(int? id) => id == null;
        protected override int MapKey(int? id) => id.Value;
        protected override int? MapKey(int id) => id;
    }
}