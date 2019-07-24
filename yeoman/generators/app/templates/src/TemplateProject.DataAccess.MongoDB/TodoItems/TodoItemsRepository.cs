﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using <%= csprojName %>.Core.Domain;
using <%= csprojName %>.Core.Interfaces.DataAccess.Repositories;
using <%= csprojName %>.Utils.Entities;
using <%= csprojName %>.Utils.Factories;

namespace <%= csprojName %>.DataAccess.MongoDB.TodoItems
{
    internal sealed class TodoItemsRepository : RepositoryBase<ITodoItem, TodoItem, TodoItemDataModel>, ITodoItemsRepository
    {
        public TodoItemsRepository(IMongoDatabase db, IMapper mapper, IFactory<TodoItem> todoItemsFactory) :
            base("TodoItems", db, mapper, todoItemsFactory)
        {

        }

        public (IList<ITodoItem> Items, int Total) GetMany(string nameFilter = null, int? limit = null, int? skip = null, string sortColumn = null, SortOrder? order = null)
        {
            var filter = FilterDefinition<TodoItemDataModel>.Empty;

            if (nameFilter != null)
                filter &= Builders<TodoItemDataModel>.Filter.Regex(nameof(TodoItemDataModel.Name), new BsonRegularExpression(nameFilter));

            var findCursor = Collection.Find(filter);

            var total = (int)findCursor.CountDocuments();

            if (sortColumn != null && order != null)
            {
                findCursor = findCursor.Sort(
                    order.Value == SortOrder.Asc ?
                         Builders<TodoItemDataModel>.Sort.Ascending(sortColumn) :
                         Builders<TodoItemDataModel>.Sort.Descending(sortColumn)
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
