using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess.Repositories;

namespace TemplateProject.DataAccess.MongoDB.TodoItems
{
    internal sealed class TodoItemsRepository : ITodoItemsRepository
    {
        private readonly IMongoDatabase _db;
        private readonly IMapper _mapper;
        private readonly Lazy<IMongoCollection<TodoItemDataModel>> _collection;

        public TodoItemsRepository(IMongoDatabase db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _collection = new Lazy<IMongoCollection<TodoItemDataModel>>(() => _db.GetCollection<TodoItemDataModel>("TodoItems"));
        }

        public IList<ITodoItem> GetAll() =>
            _mapper.Map<IList<ITodoItem>>(Collection.Find(FilterDefinition<TodoItemDataModel>.Empty).ToList());

        public ITodoItem GetById(string id)
        {
            var item = Collection.Find(x => x.Id == id).FirstOrDefault();
            if (item == null)
                return null;

            return _mapper.Map<TodoItem>(item);
        }

        public ITodoItem AddOrUpdate(ITodoItem item)
        {
            var dataModel = _mapper.Map<TodoItemDataModel>(item);
            dataModel.Id = item.Id;
            
            if (item.Id != null)
            {
                var replaceResult = Collection.ReplaceOne(x => x.Id == item.Id, dataModel);
                if (replaceResult.MatchedCount > 0)
                    return item;
            }

            Collection.InsertOne(dataModel);

            item.Id = dataModel.Id;

            return item;
        }

        public void DeleteById(string id) => Collection.DeleteOne(x => x.Id == id);

        private IMongoCollection<TodoItemDataModel> Collection => _collection.Value;
    }
}
