using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using <%= csprojName %>.Utils.Entities;
using <%= csprojName %>.Utils.Factories;

namespace <%= csprojName %>.DataAccess.MongoDB
{
    internal class RepositoryBase<TEntity, TEntityImpl, TDataModel>
        where TEntity : class, IEntity<string>
        where TEntityImpl : TEntity
        where TDataModel : class, IEntity<string>, new()
    {
        private readonly IMongoDatabase _db;
        private readonly IMapper _mapper;
        private readonly IFactory<TEntityImpl> _entitiesFactory;
        private readonly Lazy<IMongoCollection<TDataModel>> _collection;

        public RepositoryBase(string collectionName, IMongoDatabase db, IMapper mapper, IFactory<TEntityImpl> entitiesFactory)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _entitiesFactory = entitiesFactory ?? throw new ArgumentNullException(nameof(entitiesFactory));
            _collection = new Lazy<IMongoCollection<TDataModel>>(() => _db.GetCollection<TDataModel>(collectionName));
        }

        public IList<TEntity> GetAll() =>
            Collection.Find(FilterDefinition<TDataModel>.Empty).ToList().Select(Map).ToList();

        public TEntity GetById(string id)
        {
            var item = Collection.Find(x => x.Id == id).FirstOrDefault();
            if (item == null)
                return null;

            return Map(item);
        }

        public TEntity AddOrUpdate(TEntity item)
        {
            var dataModel = new TDataModel();

            Map(item, dataModel);

            if (item.Id != null)
            {
                dataModel.Id = item.Id;

                var replaceResult = Collection.ReplaceOne(x => x.Id == item.Id, dataModel);
                if (replaceResult.MatchedCount > 0)
                    return item;
            }

            Collection.InsertOne(dataModel);

            item.Id = dataModel.Id;

            return item;
        }

        public void DeleteById(string id) => Collection.DeleteOne(x => x.Id == id);

        protected IMongoCollection<TDataModel> Collection => _collection.Value;

        protected virtual void Map(TDataModel source, TEntityImpl dest) => _mapper.Map(source, dest);

        protected virtual void Map(TEntity source, TDataModel dest) => _mapper.Map(source, dest);

        protected TEntity Map(TDataModel source)
        {
            var result = _entitiesFactory.Create();

            Map(source, result);

            return result;
        }
    }
}
