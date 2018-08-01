using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TemplateProject.Utils.Entities;
using TemplateProject.Utils.Factories;

namespace TemplateProject.DataAccess.Repositories
{
    public abstract class RepositoryBase<TEntity, TEntityImpl, TKey, TDataModel>
        where TEntity : class, IEntity<TKey?>
        where TEntityImpl: TEntity
        where TKey : struct
        where TDataModel : class, IEntity<TKey>, new()
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly DbSet<TDataModel> _dbSet;
        private readonly IFactory<TEntityImpl> _entitiesFactory;

        public RepositoryBase(ApplicationDbContext db,
            IMapper mapper,
            DbSet<TDataModel> dbSet,
            IFactory<TEntityImpl> entitiesFactory)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
            _entitiesFactory = entitiesFactory ?? throw new ArgumentNullException(nameof(entitiesFactory));
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

        protected virtual void Map(TDataModel source, TEntityImpl dest)
        {
            _mapper.Map(source, dest);
        }

        protected virtual void Map(TEntity source, TDataModel dest)
        {
            _mapper.Map(source, dest);
        }

        private TEntity Map(TDataModel source)
        {
            var result = _entitiesFactory.Create();

            Map(source, result);

            return result;
        }
    }
}