using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using <%= projectNamespace %>.Core.Domain;
using <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories;
using <%= projectNamespace %>.Utils.Entities;
using <%= projectNamespace %>.Utils.Factories;

namespace <%= projectNamespace %>.DataAccess.MongoDB.TodoItems
{
    internal sealed class TodoItemsRepository : RepositoryBase<ITodoItem, TodoItem, TodoItemDataModel>, ITodoItemsRepository
    {
        public TodoItemsRepository(IMongoDatabase db, IMapper mapper, IFactory<TodoItem> todoItemsFactory) :
            base("TodoItems", db, mapper, todoItemsFactory)
        {

        }

        public (IList<ITodoItem> Items, int Total) GetMany(string nameFilter = null, int? limit = null, int? skip = null, string orderBy = null, SortOrder? orderDirection = null)
        {
            var filter = FilterDefinition<TodoItemDataModel>.Empty;

            if (nameFilter != null)
                filter &= Builders<TodoItemDataModel>.Filter.Regex(nameof(TodoItemDataModel.Name), new BsonRegularExpression(nameFilter));

            var findCursor = Collection.Find(filter);

            var total = (int)findCursor.CountDocuments();

            if (orderBy != null && orderDirection != null)
            {
                findCursor = findCursor.Sort(
                    orderDirection.Value == SortOrder.Asc ?
                         Builders<TodoItemDataModel>.Sort.Ascending(orderBy) :
                         Builders<TodoItemDataModel>.Sort.Descending(orderBy)
                );
            }

            if (skip.HasValue)
                findCursor = findCursor.Skip(skip.Value);

            if (limit.HasValue)
                findCursor = findCursor.Limit(limit.Value);

            var items = findCursor.ToList().Select(Map).ToList();

            return (items, total);
        }
    }
}
