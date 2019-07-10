using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using <%= csprojName %>.Core.Domain;
using <%= csprojName %>.Core.Interfaces.DataAccess.Repositories;

namespace <%= csprojName %>.DataAccess.MongoDB.TodoItems
{
    internal sealed class TodoItemsRepository : RepositoryBase<ITodoItem, TodoItem, TodoItemDataModel>, ITodoItemsRepository
    {
        public TodoItemsRepository(IMongoDatabase db, IMapper mapper, IFactory<TodoItem> todoItemsFactory) :
            base("TodoItems", db, mapper, todoItemsFactory)
        {

        }
    }
}
